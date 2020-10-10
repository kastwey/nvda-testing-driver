// Copyright (C) 2020 Juan José Montiel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Win32;

using Newtonsoft.Json.Linq;

using NvdaTestingDriver.Commands;
using NvdaTestingDriver.Commands.NvdaCommands;
using NvdaTestingDriver.Exceptions;
using NvdaTestingDriver.Extensions;
using NvdaTestingDriver.Settings;

namespace NvdaTestingDriver
{
 /// <summary>
 /// The main class of the package, which includes all the functionality needed to handle NVDA,
 /// send commands and get the textual response of it.
 /// </summary>
 /// <seealso cref="System.IDisposable" />
	public sealed class NvdaDriver : TrackingDisposable
	{
		private const string SetConnectionMsg = "{\"connection_type\": \"master\", \"type\": \"join\", \"channel\": \"NvdaRemote\"}\n";

		private const string SetCommunicationProtocolMsg = "{\"version\": 2, \"type\": \"protocol_version\"}";

		private const string NvdaRegistryKey = @"Software\KastweySoftware\NvdaTestingDriver";

		private const string LocalHost = "127.0.0.1";

		private const int NvdaRemotePort = 6837;

		private const string LogTimeFormat = "HH:mm:ss:fff";

		private readonly Regex _multiSpaceRegex = new Regex(@"\s+");

		private readonly NvdaDriverOptions _nvdaDriverOptions;

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "The field is being disposed in DisconnectAsync, called by Dispose.")]
		private TcpClient _tcpClient;

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "The field is being disposed in DisconnectAsync, called by Dispose.")]
		private NetworkStream _networkStream;

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0069:Disposable fields should be disposed", Justification = "The field is being disposed in DisconnectAsync, called by Dispose.")]
		private SslStream _sslStream;

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0069:Disposable fields should be disposed", Justification = "The field is being disposed in DisconnectAsync, called by Dispose.")]
		private CancellationTokenSource _cancellationTokenSource;

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0069:Disposable fields should be disposed", Justification = "The field is being disposed in DisconnectAsync, called by Dispose.")]
		private Process _nvdaProcess;

		private Task _taskReceivingMessages;

		private ILoggerFactory _loggerFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="NvdaDriver"/> class.
		/// </summary>
		/// <param name="options">The options.</param>
		public NvdaDriver(NvdaDriverOptions options)
			: base()
		{
			CheckWindowsOS();
			_nvdaDriverOptions = options ?? throw new ArgumentNullException(nameof(options));
			InitializeLogging(options);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NvdaDriver"/> class.
		/// </summary>
		public NvdaDriver()
			: this((Action<NvdaDriverOptions>)null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NvdaDriver"/> class.
		/// </summary>
		/// <param name="nvdaDriverOptions">The nvda driver options object, which you can modify in your own method.</param>
		public NvdaDriver(Action<NvdaDriverOptions> nvdaDriverOptions)
			: base()
		{
			CheckWindowsOS();
			_nvdaDriverOptions = new NvdaDriverOptions();
			nvdaDriverOptions?.Invoke(_nvdaDriverOptions);
			InitializeLogging(_nvdaDriverOptions);
		}

		/// <summary>
		/// Occurs when new data is received.
		/// </summary>
		internal event EventHandler<string> OnDataReceibed;

		/// <summary>
		/// Occurs when new speak text is received.
		/// </summary>
		internal event EventHandler<string> OnSpeakReceived;

		/// <summary>
		/// Occurs when the current speak has been cancelled.
		/// </summary>
		internal event EventHandler OnSpeakCancelled;

		/// <summary>
		/// Connects the driver asynchronously against a portable NVDA instance that will start automatically, configured with the options provided in the driver constructor.
		/// </summary>
		/// <returns>The task associated to this operation.</returns>
		/// <exception cref="NvdaTestingDriver.Exceptions.AlreadyConnectedException">Throws if the method has been called before, and the driver is already connected</exception>
		public Task ConnectAsync()
		{
			return Track(() => ConnectInternalAsync());
		}

		/// <summary>
		/// Disconnects the driver asynchronously, shutting down the NVDA instance.
		/// </summary>
		/// <returns>The task associated to this operation.</returns>
		public Task DisconnectAsync()
		{
			return Track(() => DisconnectInternalAsync());
		}

		/// <summary>
		/// Gets the next spoken message by NVDA asynchronous.
		/// </summary>
		/// <param name="actionToExecute">The action to execute, which should cause NVDA to return
		/// some message, which will be returned by this method.</param>
		/// <param name="timeout">The maximum time the method will wait for an NVDA message before raising an exception. By default, timeout is 3 seconds.</param>
		/// <param name="timeToWaitNewMessages">There are occasions when NVDA transmits messages separately, in different packets.
		/// This parameter sets the maximum time that NVDA will concatenate new messages received within the return of this method, starting from the arrival of the first message. The default time is 500 milliseconds.</param>
		/// <returns>
		/// The task that once completed, will contain The NVDA spoken text immediately following the execution of the action.
		/// </returns>
		/// <exception cref="NvdaTestingDriver.Exceptions.TimeoutException">Timeout exceeded when trying to get the next message transmitted by NVDA.</exception>
		public Task<string> GetNextSpokenMessageAsync(Func<Task> actionToExecute = null, TimeSpan? timeout = null, TimeSpan? timeToWaitNewMessages = null)
		{
			return Track<string>(() => GetNextSpokenMessageInternalAsync(actionToExecute, timeout, timeToWaitNewMessages));
		}

		/// <summary>
		/// Sends a key sequence to NVDA asynchronously.
		/// </summary>
		/// <param name="keys">The keys.</param>
		/// <returns>The task associated to this operation</returns>
		public Task SendKeySequenceAsync(params Key[] keys)
		{
			if (keys is null)
			{
				throw new ArgumentNullException(nameof(keys));
			}

			return Track(() => SendKeySequenceInternalAsync(keys));
		}

		/// <summary>
		/// Sends a key sequence to NVDA and get spoken text asynchronous.
		/// </summary>
		/// <param name="keys">The keys.</param>
		/// <returns>
		/// Te text spoken by NVDA after sending the key sequence
		/// </returns>
		public Task<string> SendKeySequenceAndGetSpokenTextAsync(params Key[] keys) =>
			SendKeySequenceAndGetSpokenTextAsync(keys, null, null);

		/// <summary>
		/// Sends a key sequence to NVDA and get spoken text asynchronous.
		/// </summary>
		/// <param name="keys">The keys.</param>
		/// <param name="timeout">The maximum time the method will wait for an NVDA message before raising an exception. By default, timeout is 3 seconds.</param>
		/// <param name="timeToWaitNewMessages">
		/// There are occasions when NVDA transmits messages separately, in different packets.
		/// This parameter sets the maximum time that NVDA will concatenate new messages received within the return of this method, starting from the arrival of the first message. The default time is 500 milliseconds.</param>
		/// <returns>
		/// Te text spoken by NVDA after sending the key sequence
		/// </returns>
		public Task<string> SendKeySequenceAndGetSpokenTextAsync(Key[] keys, TimeSpan? timeout = null, TimeSpan? timeToWaitNewMessages = null)
		{
			if (keys is null)
			{
				throw new ArgumentNullException(nameof(keys));
			}

			return Track<string>(() => SendKeySequenceAndGetSpokenTextInternalAsync(keys, timeout, timeToWaitNewMessages));
		}

		/// <summary>
		/// Sends one o more key combinations to NVDA
		/// </summary>
		/// <param name="combinations">The combinations</param>
		/// <returns>The task associated to this operation</returns>
		/// <exception cref="ArgumentNullException">combinations</exception>
		public Task SendKeyCombinationsAsync(params KeyCombination[] combinations)
		{
			if (combinations == null)
			{
				throw new ArgumentNullException(nameof(combinations));
			}

			return Track(() => SendCombinationsInternalAsync(combinations));
		}

		/// <summary>
		/// Sends key combinations to NVDA and get spoken text asynchronously.
		/// </summary>
		/// <param name="combinations">The combinations.</param>
		/// <returns>The text spoken by NVDA after sending the key combinations</returns>
		public Task<string> SendKeyCombinationsAndGetSpokenTextAsync(params KeyCombination[] combinations) =>
			SendKeyCombinationsAndGetSpokenTextAsync(combinations, null, null);

		/// <summary>
		/// Sends key combinations to NVDA and get spoken text asynchronously.
		/// </summary>
		/// <param name="combinations">The combinations.</param>
		/// <param name="timeout">The maximum time the method will wait for an NVDA message before raising an exception. By default, timeout is 3 seconds.</param>
		/// <param name="timeToWaitNewMessages">There are occasions when NVDA transmits messages separately, in different packets.
		/// This parameter sets the maximum time that NVDA will concatenate new messages received within the return of this method, starting from the arrival of the first message. The default time is 500 milliseconds.</param>
		/// <returns>The text spoken by NVDA after sending the key combinations</returns>
		public Task<string> SendKeyCombinationsAndGetSpokenTextAsync(KeyCombination[] combinations, TimeSpan? timeout, TimeSpan? timeToWaitNewMessages)
		{
			if (combinations == null)
			{
				throw new ArgumentNullException(nameof(combinations));
			}

			return Track(() => SendKeyCombinationsAndGetSpokenTextInternalAsync(combinations, timeout, timeToWaitNewMessages));
		}

		/// <summary>
		/// Sends a keystroke sequence  to NVDA asynchronously.
		/// </summary>
		/// <param name="keys">The keys.</param>
		/// <returns>The task associated to this operation</returns>
		public Task SendKeysAsync(params Key[] keys) => SendKeysAsync(false, keys);

		/// <summary>
		/// Sends the keys to NVDA (as combination) and get spoken text asynchronously.
		/// </summary>
		/// <param name="keys">The keys.</param>
		/// <returns>The text spoken by NVDA after sending the key combination</returns>
		public Task<string> SendKeysAndGetSpokenTextAsync(params Key[] keys) =>
			SendKeysAndGetSpokenTextAsync(keys, null, null);

		/// <summary>
		/// Sends the keys to NVDA (as combination) and get spoken text asynchronously.
		/// </summary>
		/// <param name="keys">The keys.</param>
		/// <param name="timeout">The maximum time the method will wait for an NVDA message before raising an exception. By default, timeout is 3 seconds.</param>
		/// <param name="timeToWaitNewMessages">There are occasions when NVDA transmits messages separately, in different packets.
		/// This parameter sets the maximum time that NVDA will concatenate new messages received within the return of this method, starting from the arrival of the first message. The default time is 500 milliseconds.</param>
		/// <returns>The text spoken by NVDA after sending the key combination</returns>
		public Task<string> SendKeysAndGetSpokenTextAsync(Key[] keys, TimeSpan? timeout, TimeSpan? timeToWaitNewMessages)
		{
			if (keys == null)
			{
				throw new ArgumentNullException(nameof(keys));
			}

			return Track(() => SendKeysAndGetSpokenTextInternalAsync(keys, timeout, timeToWaitNewMessages));
		}

		/// <summary>
		/// Send a command to NVDA without waiting for a response asynchronously.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>The task associated to this operation</returns>
		public Task SendCommandAsync(INvdaCommand command) => SendCommandAsync(command, false);

		/// <summary>
		/// Sends a command to NVDA and waits for a response that will be returned by the method asynchronously.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="timeout">The maximum time the method will wait for an NVDA message before raising an exception. By default, timeout is 3 seconds.</param>
		/// <param name="timeToWaitNewMessages">There are occasions when NVDA transmits messages separately, in different packets.
		/// This parameter sets the maximum time that NVDA will concatenate new messages received within the return of this method, starting from the arrival of the first message. The default time is 500 milliseconds.</param>
		/// <returns>
		/// The text sopen by NVDA after sending the command
		/// </returns>
		/// <exception cref="ArgumentNullException">command</exception>
		public Task<string> SendCommandAndGetSpokenTextAsync(INvdaCommand command, TimeSpan? timeout = null, TimeSpan? timeToWaitNewMessages = null)
		{
			if (command == null)
			{
				throw new ArgumentNullException(nameof(command));
			}

			Logger.LogInformation($"Executing SendCommandAndGetSpokenTextAsync. Command: {command.GetDescription()}.");

			var combinationSet = GetKeyCombinationSet(command);
			return Track(() => SendKeyCombinationSetAndGetSpokenTextInternalAsync(combinationSet, timeout, timeToWaitNewMessages));
		}

		/// <summary>
		/// Cancels the current speak asynchronously.
		/// </summary>
		/// <param name="timeout">The timeout for this operation. By default, timeout is 3 seconds.</param>
		/// <returns>
		/// The task associated to this operation
		/// </returns>
		/// <exception cref="System.TimeoutException">Timeout while waiting for stopping speech.</exception>
		public Task StopReadingAsync(TimeSpan? timeout = null)
		{
			return Track(() => StopReadingInternalAsync(timeout));
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		public override void Dispose()
		{
			Logger.LogInformation("Starting to dispose NvdaTestingDriver...");
			base.Dispose();
		}

		/// <summary>
		/// Finishes the dispose asynchronous.
		/// </summary>
		/// <returns>The task associated to this operation</returns>
		protected override async Task FinishDisposeAsync()
		{
			await DisconnectInternalAsync(true);
		}

		/// <summary>
		/// Kills all previous nvda processes.
		/// </summary>
		private void KillPreviousNVDA()
		{
			var nvdaProcesses = Process.GetProcessesByName("nvda");
			if (nvdaProcesses.Any())
			{
				foreach (var nvdaProcess in nvdaProcesses)
				{
					try
					{
						nvdaProcess.Kill();
					}
					catch (Exception ex) when (ex is InvalidOperationException || ex is Win32Exception)
					{
						Console.WriteLine($"error while killing nvda process: {ex.Message} We continue.");
					}
				}
			}
		}

		/// <summary>
		/// Initializes the logging system.
		/// If options.LoggerFactory is already present, we create the ILogger object directly.
		/// If the options.LoggerFactory object has not been adjusted, we create an ILoggerFactory object, adding the Debug and Console providers.
		/// </summary>
		/// <param name="options">The NVDA options.</param>
		private void InitializeLogging(NvdaDriverOptions options)
		{
			if (options.EnableLogging && options?.LoggerFactory != null)
			{
				_loggerFactory = options.LoggerFactory;
				Logger = _loggerFactory.CreateLogger("NvdaTestingDriver");
			}
			else if (options.EnableLogging)
			{
				throw new ArgumentException("If you enable the logging, you must specify the logger factory to be used to save the logs.");
			}
			else
			{
				_loggerFactory = NullLoggerFactory.Instance;
			}

			Logger = _loggerFactory.CreateLogger("NvdaTestingDriver");
		}

		/// <summary>
		/// Checks whether the current OS is windows.
		/// </summary>
		/// <exception cref="NotSupportedException">This library is only compatible with Windows OS, because the screen reader used for testing is only compatible with it.</exception>
		private void CheckWindowsOS()
		{
			if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				throw new NotSupportedException("This library is only compatible with Windows OS, because the screen reader used for testing is only compatible with it.");
			}
		}

		/// <summary>
		/// Gets the nvda executable file path from windows registry. If registry value does not exists,
		/// it will extract the nvda.zip file into a temporary folder..
		/// </summary>
		/// <returns>The NVDA executable file path</returns>
		private string GetNvdaExecutableFilePath()
		{
			using (var registryKey = Registry.CurrentUser.OpenSubKey(NvdaRegistryKey))
			{
				if (registryKey == null)
				{
					Logger.LogDebug("This is the first time this NVDA version is started on this machine.");
					return ExtractNvdaZipFileInTmpDir();
				}

				var nvdaExecutableFilePath = (string)registryKey.GetValue(GetNvdaVersion());
				if (string.IsNullOrWhiteSpace(nvdaExecutableFilePath))
				{
					Logger.LogDebug("This is the first time this NVDA version is started on this machine.");
					return ExtractNvdaZipFileInTmpDir();
				}

				CheckNvdaIntegrity(nvdaExecutableFilePath);
				return nvdaExecutableFilePath;
			}
		}

		/// <summary>
		/// Checks the nvda SR integrity, restoring the missing files from temporary directory.
		/// </summary>
		/// <param name="nvdaExecutableFilePath">The nvda executable file path.</param>
		private void CheckNvdaIntegrity(string nvdaExecutableFilePath)
		{
			Logger.LogTrace("Starting integrity checks...");
			var nvdaDirectory = Path.GetDirectoryName(nvdaExecutableFilePath);
			using (var nvdaZipStream = GetType().Assembly.GetManifestResourceStream("NvdaTestingDriver.nvda.zip"))
			{
				using (var zipArchive = new ZipArchive(nvdaZipStream))
				{
					var entries = zipArchive.Entries.Where(e => !e.FullName.EndsWith("/", StringComparison.OrdinalIgnoreCase));
					var entriesCount = entries.Count();
					var currentFiles = !Directory.Exists(nvdaDirectory) ? new List<string>() : Directory.GetFiles(nvdaDirectory, "*.*", SearchOption.AllDirectories).Select(f => f.Substring(nvdaDirectory.Length + 1).Replace("\\", "/"));
					var currentFilesCount = currentFiles.Count();
					if (entriesCount != currentFilesCount)
					{
						var nonExistingEntries = entries.Where(e => !currentFiles.Contains(e.FullName));
						if (nonExistingEntries.Any())
						{
							Logger.LogTrace($"Looks like there's some deleted files. Restoring {nonExistingEntries.Count()} files...");
							foreach (var entry in nonExistingEntries)
							{
								var fileName = Path.Combine(nvdaDirectory, entry.FullName);
								var directory = Path.GetDirectoryName(fileName);
								if (!Directory.Exists(directory))
								{
									Directory.CreateDirectory(directory);
								}

								entry.ExtractToFile(fileName);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Extracts the nvda zip file into a temporary dir.
		/// </summary>
		/// <returns>The path to the NVDA executable program</returns>
		private string ExtractNvdaZipFileInTmpDir()
		{
			string nvdaExecutableFilePath = null;
			var nvdaVersion = GetNvdaVersion();

			using (var nvdaZipStream = GetType().Assembly.GetManifestResourceStream("NvdaTestingDriver.nvda.zip"))
			{
				using (var zipArchive = new ZipArchive(nvdaZipStream))
				{
					var tmpDir = Path.Combine(Path.GetTempPath(), nvdaVersion);
					nvdaExecutableFilePath = Path.Combine(tmpDir, "nvda.exe");
					Logger.LogTrace("Extracting NVDA zip file. This may take a few seconds...");
					zipArchive.ExtractToDirectory(tmpDir);
					Logger.LogTrace("Zip extracted.");
					using (var registryKey = Registry.CurrentUser.CreateSubKey(NvdaRegistryKey, true))
					{
						registryKey.SetValue(nvdaVersion, nvdaExecutableFilePath, RegistryValueKind.String);
						Logger.LogTrace($"Registry key adjusted to Nvda Version {nvdaVersion}, to executable path {nvdaExecutableFilePath}.");
					}
				}
			}

			return nvdaExecutableFilePath;
		}

		/// <summary>
		/// Gets the nvda version to name the temporary NVDA folder, depending on the current assembly version.
		/// </summary>
		/// <returns>The nvda version regarding the assembly version</returns>
		private string GetNvdaVersion()
		{
			return $"nvda_{GetType().Assembly.GetName().Version}";
		}

		/// <summary>
		/// Checks the NVDA driver connectivity.
		/// </summary>
		/// <exception cref="NvdaTestingDriver.Exceptions.NotConnectedException">The driver is not yet connected. Use ConnectAsync to do that.</exception>
		private void CheckConnectivity()
		{
			if (!_tcpClient.Connected || _networkStream == null || _sslStream == null || !_sslStream.CanWrite)
			{
				throw new NotConnectedException("The driver is not yet connected. Use ConnectAsync to do that.");
			}
		}

		/// <summary>
		/// Connects the socket to NVDA remote server asynchronously.
		/// </summary>
		/// <param name="host">The host.</param>
		/// <param name="port">The port.</param>
		/// <param name="timeout">The timeout.</param>
		/// <returns>The task associated to this operation.</returns>
		private async Task ConnectSocketAsync(string host, int port, TimeSpan? timeout = null)
		{
			if (timeout == null)
			{
				timeout = TimeSpan.FromSeconds(10);
			}

			var socketAvailable = false;
			var operationStart = DateTime.Now;
			do
			{
				try
				{
					await _tcpClient.ConnectAsync(host, port);
					Logger.LogTrace("TCP client connected.");
					socketAvailable = true;
				}
				catch (SocketException ex)
				{
					if ((DateTime.Now - operationStart) > timeout)
					{
						Logger.LogError($"Timeout exception while connecting to NVDA remote server. {ex.GetType()}: {ex.Message}.");
						throw;
					}

					Logger.LogWarning($"Retrying TCP connection to Nvda remote server. Previous attempt raised the exception: {ex.GetType()}: {ex.Message}.");
					Thread.Sleep(500);
				}
			}
			while (!socketAvailable);
		}

		/// <summary>
		/// Writes a message to nvda remote sockets asynchronously.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns>The task associated to this operation.</returns>
		private async Task WriteMessageAsync(string message)
		{
			if (!message.EndsWith("\n", StringComparison.OrdinalIgnoreCase))
			{
				message += "\n";
			}

			Logger.LogTrace($"Sending message: \"{message}\"");
			var buffer = Encoding.UTF8.GetBytes(message.ToCharArray(), 0, message.Length);
			await _sslStream.WriteAsync(buffer, 0, buffer.Length, _cancellationTokenSource.Token);
		}

		/// <summary>
		/// Starts the thread that will read incoming messages from the socket connected to NVDA Remote, allowing it to execute events dependent on the arrival of new messages.
		/// </summary>
		private void BeginReadMessages(CancellationToken token)
		{
			token.Register(() =>
			{
				_sslStream.Close();
				_sslStream.Dispose();
				_networkStream.Close();
				_networkStream.Dispose();
				_tcpClient.Close();
				_tcpClient.Dispose();
			});
			Logger.LogTrace("Starting background task to read NVDA messages from TCP...");
			_taskReceivingMessages = Task.Run(async () =>
			{
				while (!token.IsCancellationRequested)
				{
					var buffer = new byte[1024];
					var messageData = new StringBuilder();
					var bytes = -1;
					do
					{
						if (token.IsCancellationRequested)
						{
							return;
						}

						if (!_sslStream.CanRead)
						{
							return;
						}

						// Read the client's test message.
						Logger.LogTrace("Requesting more bytes from ssl stream...");
						bytes = await _sslStream.ReadAsync(buffer, 0, buffer.Length, token);
						Logger.LogTrace($"Read {bytes} bytes from ssl stream.");

						// Use Decoder class to convert from bytes to UTF8
						// in case a character spans two buffers.
						var decoder = Encoding.UTF8.GetDecoder();
						var chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
						decoder.GetChars(buffer, 0, bytes, chars, 0);
						Logger.LogTrace($"String received: \"{new string(chars)}\".");

						messageData.Append(chars);
					}
					while (bytes == buffer.Length && token.IsCancellationRequested);
					if (token.IsCancellationRequested)
					{
						return;
					}

					var message = messageData.ToString();
					ParseMessage(message);
					OnDataReceibed?.Invoke(this, message);
				}
			});
		}

		/// <summary>
		/// Parses the message received from NVDA remote server.
		/// </summary>
		/// <param name="message">The message.</param>
		private void ParseMessage(string message)
		{
			if (string.IsNullOrWhiteSpace(message))
			{
				return;
			}

			var jsonMessage = JObject.Parse(message);
			var type = jsonMessage["type"].Value<string>();
			if (string.IsNullOrWhiteSpace(type))
			{
				return;
			}

			switch (type)
			{
				case "speak":
					ParseSpeak((JArray)jsonMessage["sequence"]);
					break;
				case "cancel":
					ParseSpeak(jsonMessage);
					break;
			}
		}

		/// <summary>
		/// Parses the speak messages and raises the OnSpeakReceived and OnSpeakCancelled events.
		/// </summary>
		/// <param name="messages">The messages.</param>
		private void ParseSpeak(JToken messages)
		{
			Logger.LogTrace($"Parsing speak sequence. json array received: \"{messages}\".");

			if (messages == null)
			{
				Logger.LogTrace("The speak sequence is null.");
				return;
			}

			if (messages.Type == JTokenType.Object && messages["type"].Value<string>() == "cancel")
			{
				OnSpeakCancelled?.Invoke(this, EventArgs.Empty);
				return;
			}

			var finalMessage = new StringBuilder();
			Logger.LogTrace($"Iterating throught sequence elements...");
			foreach (var token in messages)
			{
				Logger.LogTrace($"Message type: {token.Type}.");
				if (token.Type == JTokenType.String)
				{
					Logger.LogTrace($"Adding message to final message: \"{token.Value<string>()}\".");
					finalMessage.AppendLine(SanitizeNvdaSpokenMessage(token.Value<string>()));
				}
			}

			if (finalMessage.Length > 0)
			{
				var processedMessage = finalMessage.ToString();
				OnSpeakReceived?.Invoke(this, processedMessage);
			}
		}

		/// <summary>
		/// Sanitizes the nvda spoken message to remove unnecesary characters.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns>The sanitized message</returns>
		private string SanitizeNvdaSpokenMessage(string message)
		{
			message = message.Trim(new char[] { ' ', '\r', '\n', '	' });
			message = _multiSpaceRegex.Replace(message, " ");
			return WebUtility.UrlDecode(message);
		}

		/// <summary>
		/// Convert a <see cref="Key"/> into Json string to be written in the socket.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="down">if set to <c>true</c> [down].</param>
		/// <returns>Returns the json representation of the key</returns>
		private string ToJsonKey(Key key, bool down)
		{
			return $"{{\"scan_code\": {key.ScanCode}, \"extended\": {key.Extended.ToString(CultureInfo.InvariantCulture).ToLowerInvariant()}, " +
				$"\"vk_code\": {key.KeyCode}, \"pressed\": {down.ToString(CultureInfo.InvariantCulture).ToLowerInvariant()}, \"type\": \"key\"}}";
		}

		/// <summary>
		/// Gets the key combination set from a command, checking the command is well constructed.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>the right combination set depending on current keyboard layout</returns>
		/// <exception cref="ArgumentNullException">command</exception>
		/// <exception cref="NvdaTestingDriver.Exceptions.InvalidCommandException">
		/// The command has not  the current keyboard layout combination set
		/// or
		/// There are no keys defined in the  key combination set
		/// </exception>
		private List<KeyCombination> GetKeyCombinationSet(INvdaCommand command)
		{
			if (command == null)
			{
				throw new ArgumentNullException(nameof(command));
			}

			var combinationSet = _nvdaDriverOptions.KeyboardSettings.KeyboardLayout == KeyboardLayout.Desktop ? command.DesktopCombinationSet : command.LaptopCombinationSet;
			var keyboardLayoutStr = _nvdaDriverOptions.KeyboardSettings.KeyboardLayout.ToString();

			if (combinationSet == null || command.DesktopCombinationSet.Count == 0)
			{
				throw new InvalidCommandException($"The command has not {keyboardLayoutStr} combination set.");
			}

			if (!combinationSet.SelectMany(c => c).Any())
			{
				throw new InvalidCommandException($"There are no keys defined in the {keyboardLayoutStr} combination sets.");
			}

			return combinationSet;
		}

		/// <summary>
		/// Sends a set of key combinations and get spoken text  by NVDA.
		/// </summary>
		/// <param name="combinationSet">The combination set.</param>
		/// <param name="timeout">The maximum time the method will wait for an NVDA message before raising an exception. By default, timeout is 3 seconds.</param>
		/// <param name="timeToWaitNewMessages">There are occasions when NVDA transmits messages separately, in different packets.
		/// This parameter sets the maximum time that NVDA will concatenate new messages received within the return of this method, starting from the arrival of the first message. The default time is 500 milliseconds.</param>
		/// <returns>The task that will contain the spoken message</returns>
		private async Task<string> SendKeyCombinationSetAndGetSpokenTextInternalAsync(List<KeyCombination> combinationSet, TimeSpan? timeout, TimeSpan? timeToWaitNewMessages)
		{
			if (timeout is null)
			{
				timeout = _nvdaDriverOptions.DefaultTimeoutt;
			}

			if (timeToWaitNewMessages is null)
			{
				timeToWaitNewMessages = _nvdaDriverOptions.DefaultTimeoutWaitingForNewMessages;
			}

			await StopReadingAsync(timeout);
			return await GetNextSpokenMessageAsync(
				async () =>
			{
				foreach (var combination in combinationSet.Where(c => c.Any()))
				{
					await SendKeysAsync(combination.ToArray());
				}
			}, timeout, timeToWaitNewMessages);
		}

		/// <summary>
		/// Sends the key combinations and get spoken text asynchronously (internal use).
		/// </summary>
		/// <param name="combinations">The combinations.</param>
		/// <param name="timeout">The maximum time the method will wait for an NVDA message before raising an exception. By default, timeout is 3 seconds.</param>
		/// <param name="timeToWaitNewMessages">There are occasions when NVDA transmits messages separately, in different packets.
		/// This parameter sets the maximum time that NVDA will concatenate new messages received within the return of this method, starting from the arrival of the first message. The default time is 500 milliseconds.</param>
		/// <returns>The text spoken by NVDA after sending the key combinations</returns>
		private async Task<string> SendKeyCombinationsAndGetSpokenTextInternalAsync(KeyCombination[] combinations, TimeSpan? timeout = null, TimeSpan? timeToWaitNewMessages = null)
		{
			await StopReadingAsync(timeout);
			return await GetNextSpokenMessageAsync(
				() =>
				{
					return SendKeyCombinationsAsync(combinations);
				},
				timeout,
				timeToWaitNewMessages);
		}

		/// <summary>
		/// Sends a set of key combinations to NVDA.
		/// </summary>
		/// <param name="combinationSet">The combination set.</param>
		/// <param name="disposing">if set to <c>true</c> [disposing].</param>
		private async Task SendKeyCombinationSetAsync(List<KeyCombination> combinationSet, bool disposing = false)
		{
			foreach (var combination in combinationSet.Where(c => c.Any()))
			{
				await SendKeysAsync(disposing, combination.ToArray());
			}
		}

		/// <summary>
		/// Send a command to NVDA without waiting for a response asynchronously.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="disposing">if set to <c>true</c>, it means that the object is being disposed, and therefore active task tracking will no longer be used, as the function is being executed as part of the NVDA dispose..</param>
		/// <returns>
		/// The task associated to this operation
		/// </returns>
		/// <exception cref="ArgumentNullException">command</exception>
		private Task SendCommandAsync(INvdaCommand command, bool disposing)
		{
			var logPrefix = "SendCommandAsync: ";

			if (command == null)
			{
				throw new ArgumentNullException(nameof(command));
			}

			Logger.LogInformation($"{logPrefix}Executing SendCommandAsync. Command: {command.GetDescription()}.");

			var combinationSet = GetKeyCombinationSet(command);

			// If the disconnection occurs as a result of disposing the NvdaTestingDriver object, we execute the command directly, without calling the task tracking  function.
			if (disposing)
			{
				Logger.LogTrace($"{logPrefix}The object is being disposed. Calling to \"SendKeyCombinationSetAsync\" without tracking the task.");
				return SendKeyCombinationSetAsync(combinationSet, disposing);
			}

			return Track(() => SendKeyCombinationSetAsync(combinationSet));
		}


		/// <summary>
		/// Sends the key combinations asynchronousl (internal use).
		/// </summary>
		/// <param name="combinations">The combinations.</param>
		/// <returns>The task associated to this operation</returns>
		private async Task SendCombinationsInternalAsync(KeyCombination[] combinations)
		{
			var logPrefix = "SendCombinationsAsync: ";
			Logger.LogInformation($"{logPrefix}Executing SendCombinationsAsync" +
				"Combinations: " +
				string.Join(", ", combinations.Select(c => "(" + c.GetDescription() + ")")) +
				".");

			foreach (var combination in combinations)
			{
				await SendKeysAsync(combination.ToArray());
			}
		}


		/// <summary>
		/// Sends a keystroke sequence  to NVDA asynchronously.
		/// </summary>
		/// <param name="disposing">if set to <c>true</c>, it means that the object is being disposed, and therefore active task tracking will no longer be used, as the function is being executed as part of the NVDA dispose..</param>
		/// <param name="keys">The keys.</param>
		/// <returns>
		/// The task associated to this operation
		/// </returns>
		/// <exception cref="ArgumentNullException">keys</exception>
		private Task SendKeysAsync(bool disposing, params Key[] keys)
		{
			var logPrefix = "SendKeysAsync: ";
			if (keys == null)
			{
				throw new ArgumentNullException(nameof(keys));
			}

			Logger.LogInformation($"{logPrefix}Executing SendKeysAsync. Keys: {string.Join("+", keys.Select(k => k.Name))}. Disposing: {disposing}.");
			if (disposing)
			{
				Logger.LogTrace($"{logPrefix}The object is being disposed, so the function won't be executed within the task tracking.");
				return SendKeysInternalAsync(keys);
			}

			return Track(() => SendKeysInternalAsync(keys));
		}

		/// <summary>
		/// Sends the keys to NVDA asynchronously (internal use).
		/// </summary>
		/// <param name="keys">The keys.</param>
		/// <returns>The task associated to this operation</returns>
		private async Task SendKeysInternalAsync(Key[] keys)
		{
			CheckConnectivity();

			foreach (var k in keys)
			{
				Logger.LogTrace($"Sending key Down: {k.Name}.");
				await WriteMessageAsync(ToJsonKey(k, true));
			}

			foreach (var k in keys.Reverse())
			{
				Logger.LogTrace($"Sending key up: {k.Name}.");
				await WriteMessageAsync(ToJsonKey(k, false));
			}
		}

		/// <summary>
		/// Sends the keys to NVDA and gets the spoken text asynchronously.
		/// </summary>
		/// <param name="keys">The keys.</param>
		/// <param name="timeout">The maximum time the method will wait for an NVDA message before raising an exception. By default, timeout is 3 seconds.</param>
		/// <param name="timeToWaitNewMessages">There are occasions when NVDA transmits messages separately, in different packets.
		/// This parameter sets the maximum time that NVDA will concatenate new messages received within the return of this method, starting from the arrival of the first message. The default time is 500 milliseconds.</param>
		/// <returns>The text spoken by NVDA after sending keys</returns>
		private async Task<string> SendKeysAndGetSpokenTextInternalAsync(Key[] keys, TimeSpan? timeout, TimeSpan? timeToWaitNewMessages)
		{
			if (timeout is null)
			{
				timeout = _nvdaDriverOptions.DefaultTimeoutt;
			}

			if (timeToWaitNewMessages is null)
			{
				timeToWaitNewMessages = _nvdaDriverOptions.DefaultTimeoutWaitingForNewMessages;
			}

			await StopReadingAsync(timeout);
			return await GetNextSpokenMessageAsync(
				() =>
			{
				return SendKeysAsync(keys);
			},
				timeout,
			timeToWaitNewMessages);
		}

		/// <summary>
		/// Connects the driver asynchronously against a portable NVDA instance that will start automatically, configured with the options provided in the driver constructor.
		/// </summary>
		/// <returns>The task associated to this operation.</returns>
		/// <exception cref="NvdaTestingDriver.Exceptions.AlreadyConnectedException">Throws if the method has been called before, and the driver is already connected</exception>
		private async Task ConnectInternalAsync()
		{
			Logger.LogInformation("Starting NVDA connection.");
			if (_tcpClient != null && _tcpClient.Connected)
			{
				Logger.LogError("The driver is already connected.");
				throw new AlreadyConnectedException();
			}

			var nvdaExecutableFilePath = GetNvdaExecutableFilePath();
			var nvdaDirectory = Path.GetDirectoryName(nvdaExecutableFilePath);
			var nvdaDriverOptionsWriter = new NvdaDriverOptionsWriter(_nvdaDriverOptions);
			Logger.LogTrace("Writing options into NVDA ini file...");
			nvdaDriverOptionsWriter.WriteOptionsToIniFile(Path.Combine(nvdaDirectory, "userConfig", "nvda.ini"));
			Logger.LogTrace("Killing previous NVDA process to avoid multi-instance problems.");
			KillPreviousNVDA();
			Logger.LogTrace("Starting NVDA process...");
			var processParam = new ProcessStartInfo { FileName = nvdaExecutableFilePath };
			_nvdaProcess = Process.Start(processParam);
			_nvdaProcess.WaitForInputIdle();
			Logger.LogTrace("Nvda estarted.");
			Logger.LogTrace("Starting TCP connection to Nvda Remote Server...");
			_tcpClient = new TcpClient();
			await ConnectSocketAsync(LocalHost, NvdaRemotePort);
			_networkStream = _tcpClient.GetStream();
			_sslStream = new SslStream(_networkStream, false, (sender, certificate, chain, sslPolicyErrors) => true, null);
			Logger.LogTrace("Authenticating SSL connection...");
			await _sslStream.AuthenticateAsClientAsync("nvdaremote.com");
			_cancellationTokenSource = new CancellationTokenSource();
			Logger.LogTrace("Start reading messages from remote server.");
			BeginReadMessages(_cancellationTokenSource.Token);
			await WriteMessageAsync(SetCommunicationProtocolMsg);
			await WriteMessageAsync(SetConnectionMsg);
		}

		/// <summary>
		/// Disconnects the driver asynchronously, shutting down the NVDA instance.
		/// </summary>
		/// <returns>The task associated to this operation.</returns>
		private async Task DisconnectInternalAsync(bool disposing = false)
		{
			Logger.LogInformation("Disconnecting NvdaTestingDriver...");
			if (_sslStream != null && _sslStream.CanWrite)
			{
				await ShutdownNvdaAsync(disposing);
			}

			Logger.LogInformation("Nvda disconnected...");
			Logger.LogTrace("Canceling pending tasks...");
			_cancellationTokenSource.Cancel();
			if (_taskReceivingMessages != null)
			{
				await Task.WhenAll(_taskReceivingMessages);
			}

			_cancellationTokenSource?.Dispose();
			Logger.LogTrace("Waiting to NVDA for exiting...");
			_nvdaProcess?.WaitForExit(10000);
			try
			{
				Logger.LogTrace("Killing NVDA process (only if the quick command failed)...");
				_nvdaProcess?.Kill();
			}
			catch (Exception ex) when (ex is InvalidOperationException || ex is Win32Exception)
			{
				// The process has been closed when exiting due the prior statement (WaitForExit).
				Logger.LogTrace("The process was already closed by itself.");
			}

			_nvdaProcess?.Dispose();
			Logger.LogInformation("Nvda disconnected and closed successfully.");
		}

		/// <summary>
		/// Gets the next spoken message by NVDA asynchronous.
		/// </summary>
		/// <param name="actionToExecute">The action to execute, which should cause NVDA to return
		/// <param name="timeout">The maximum time the method will wait for an NVDA message before raising an exception. By default, timeout is 3 seconds.</param>
		/// <param name="timeToWaitNewMessages">
		/// There are occasions when NVDA transmits messages separately, in different packets.
		/// This parameter sets the maximum time that NVDA will concatenate new messages received within the return of this method, starting from the arrival of the first message. The default time is 500 milliseconds.</param>
		/// some message, which will be returned by this method.</param>
		/// <returns>The task that once completed, will contain The NVDA spoken text immediately following the execution of the action.</returns>
		/// <exception cref="NvdaTestingDriver.Exceptions.TimeoutException">Timeout exceeded when trying to get the next message transmitted by NVDA.</exception>
		private async Task<string> GetNextSpokenMessageInternalAsync(Func<Task> actionToExecute = null, TimeSpan? timeout = null, TimeSpan? timeToWaitNewMessages = null)
		{
			var logPrefix = "GetNextSpokenMessageAsync: ";

			if (timeout is null)
			{
				timeout = _nvdaDriverOptions.DefaultTimeoutt;
			}

			if (timeToWaitNewMessages is null)
			{
				timeToWaitNewMessages = _nvdaDriverOptions.DefaultTimeoutWaitingForNewMessages;
			}

			Logger.LogInformation($"{logPrefix}Executting: GetNextSpokenMessageAsync.");

			CheckConnectivity();

			var spokenMessage = new StringBuilder();
			DateTime? firstMessageTime = null;

			void LocalSpeakReceived(object sender, string message)
			{
				Logger.LogDebug($"{logPrefix}Event received: OnSpeakReceived. Message: \"{message}\"");
				if (!string.IsNullOrWhiteSpace(message))
				{
					if (spokenMessage.Length == 0)
					{
						firstMessageTime = DateTime.Now;
						Logger.LogTrace($"{logPrefix}First message: {firstMessageTime.Value.ToString(LogTimeFormat)}.");
					}

					spokenMessage.Append((spokenMessage.Length > 0 ? Environment.NewLine : string.Empty) + message);
				}
			}

			OnSpeakReceived += LocalSpeakReceived;
			if (actionToExecute != null)
			{
				Logger.LogDebug($"{logPrefix}Executing action before receiving speak messages...");
				await actionToExecute.Invoke();
				Logger.LogDebug($"{logPrefix}Action executed.");
			}

			var operationStart = DateTime.Now;
			Logger.LogTrace($"{logPrefix}Starting operation at {operationStart.ToString(LogTimeFormat)}.");
			while ((spokenMessage.Length == 0 || (firstMessageTime.HasValue && DateTime.Now - firstMessageTime.Value < timeToWaitNewMessages))
				&& DateTime.Now - operationStart < timeout)
			{
				Logger.LogTrace($"{logPrefix}Waiting for new messages. Delaying 500 MS...");
				await Task.Delay(100);
			}

			Logger.LogTrace($"{logPrefix}Waiting for new messages finished.");
			OnSpeakReceived -= LocalSpeakReceived;
			if (spokenMessage.Length == 0 && DateTime.Now - operationStart > timeout)
			{
				Logger.LogError($"Error receiving spoken message. The defined timeout was exceeded: {timeout.Value.TotalMilliseconds} milliseconds.");
				throw new Exceptions.TimeoutException($"Timeout exceeded when trying to get the next message transmitted by NVDA. Timeout: {timeout}.");
			}

			Logger.LogInformation($"Final spoken message: \"{spokenMessage}\"");
			return spokenMessage.ToString();
		}

		/// <summary>
		/// Sends a key sequence to NVDA asynchronously.
		/// </summary>
		/// <param name="keys">The keys.</param>
		/// <returns>The task associated to this operation</returns>
		private async Task SendKeySequenceInternalAsync(params Key[] keys)
		{
			Logger.LogInformation("Executing SendKeySequenceAsync. Keys: " +
				string.Join(", ", keys.Select(k => k.Name)) + ".");
			foreach (var key in keys)
			{
				await SendKeysAsync(key);
			}
		}

		/// <summary>
		/// Sends a key sequence to NVDA and get spoken text asynchronous.
		/// </summary>
		/// <param name="keys">The keys.</param>
		/// <param name="timeout">The maximum time the method will wait for an NVDA message before raising an exception. By default, timeout is 3 seconds.</param>
		/// <param name="timeToWaitNewMessages">
		/// There are occasions when NVDA transmits messages separately, in different packets.
		/// This parameter sets the maximum time that NVDA will concatenate new messages received within the return of this method, starting from the arrival of the first message. The default time is 500 milliseconds.</param>
		/// <returns>
		/// Te text spoken by NVDA after sending the key sequence
		/// </returns>
		private async Task<string> SendKeySequenceAndGetSpokenTextInternalAsync(Key[] keys, TimeSpan? timeout = null, TimeSpan? timeToWaitNewMessages = null)
		{
			var logPrefix = "SendKeySequenceAndGetSpokenTextAsync: ";

			Logger.LogInformation($"{logPrefix}Executing SendKeySequenceAndGetSpokenText. Keys: " +
				string.Join(", ", keys.Select(k => k.Name)) + ".");

			await StopReadingAsync(timeout);
			return await GetNextSpokenMessageAsync(
				() =>
			{
				return SendKeySequenceAsync(keys);
			},
				timeout,
				timeToWaitNewMessages);
		}

		/// <summary>
		/// Cancels the current speak asynchronously.
		/// </summary>
		/// <param name="timeout">The timeout for this operation. By default, timeout is 3 seconds.</param>
		/// <returns>
		/// The task associated to this operation
		/// </returns>
		/// <exception cref="System.TimeoutException">Timeout while waiting for stopping speech.</exception>
		private async Task StopReadingInternalAsync(TimeSpan? timeout = null)
		{
			Logger.LogInformation("Executing StopReadingAsync.");

			if (timeout == null)
			{
				timeout = _nvdaDriverOptions.DefaultTimeoutt;
			}

			var cancelReceived = false;
			void ONLocalSpeakCancelled(object sender, EventArgs e)
			{
				Logger.LogTrace("Event received: OnSpeakCancelled.");
				cancelReceived = true;
			}

			OnSpeakCancelled += ONLocalSpeakCancelled;
			var operationStart = DateTime.Now;
			await SendKeysAsync(Key.Control);
			while (!cancelReceived && (DateTime.Now - operationStart) < timeout)
			{
				Logger.LogTrace("Sending control (we don't received cancelled signal yet...");
				await Task.Delay(100);
				await SendKeysAsync(Key.Control);
			}

			if ((DateTime.Now - operationStart) > timeout)
			{
				throw new Exceptions.TimeoutException("Timeout while waiting for stopping speech.");
			}

			OnSpeakCancelled -= ONLocalSpeakCancelled;
		}

		/// <summary>
		/// Shutdowns the nvda instance, by sending a Nvda+Q keystroke.
		/// </summary>
		/// <returns>The task associated to this operation</returns>
		private Task ShutdownNvdaAsync(bool disposing = false)
		{
			if (disposing)
			{
				return SendKeyCombinationSetAsync(GetKeyCombinationSet(BasicCommands.QuitNvda), disposing);
			}

			return SendCommandAsync(BasicCommands.QuitNvda);
		}
	}
}

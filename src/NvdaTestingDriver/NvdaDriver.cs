// Copyright (C) 2019 Juan José Montiel
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
	public class NvdaDriver : TrackingDisposable
	{
		private const string SetConnectionMsg = "{\"connection_type\": \"master\", \"type\": \"join\", \"channel\": \"NvdaRemote\"}\n";

		private const string SetCommunicationProtocolMsg = "{\"version\": 2, \"type\": \"protocol_version\"}";

		private const string NvdaRegistryKey = @"Software\KastweySoftware\NvdaTestingDriver";

		private const string LocalHost = "127.0.0.1";

		private const int NvdaRemotePort = 6837;

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
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0069:Disposable fields should be disposed", Justification = "This object can be passed by reference from other class, so it shouldn't be disposed internally.")]
		private ILoggerFactory _loggerFactory;

		private ILogger _logger;

		private bool _internalLoggerFactory;

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
		/// Gets the next spoken message by NVDA asynchronously.
		/// </summary>
		/// <param name="actionToExecute">The action to execute, which should cause NVDA to return
		/// some message, which will be returned by this method.</param>
		/// <returns>The task that once completed, will contain The NVDA spoken text immediately following the execution of the action.</returns>
		public Task<string> GetNextSpokenMessageAsync(Func<Task> actionToExecute)
		{
			if (actionToExecute is null)
			{
				throw new ArgumentNullException(nameof(actionToExecute));
			}

			return Track<string>(() => GetNextSpokenMessageInternalAsync(actionToExecute));
		}

		/// <summary>
		/// Gets the next spoken message by NVDA asynchronous.
		/// </summary>
		/// <param name="timeout">The maximum time the method will wait for an NVDA message before raising an exception. By default, timeout is 3 seconds.</param>
		/// <param name="timeToWaitNewMessages">
		/// There are occasions when NVDA transmits messages separately, in different packets.
		/// This parameter sets the maximum time that NVDA will concatenate new messages received within the return of this method, starting from the arrival of the first message.</param>
		/// <param name="actionToExecute">The action to execute, which should cause NVDA to return
		/// some message, which will be returned by this method.</param>
		/// <returns>The task that once completed, will contain The NVDA spoken text immediately following the execution of the action.</returns>
		/// <exception cref="NvdaTestingDriver.Exceptions.TimeoutException">Timeout exceeded when trying to get the next message transmitted by NVDA.</exception>
		public Task<string> GetNextSpokenMessageAsync(TimeSpan? timeout = null, TimeSpan? timeToWaitNewMessages = null, Func<Task> actionToExecute = null)
		{
			return Track<string>(() => GetNextSpokenMessageInternalAsync(timeout, timeToWaitNewMessages, actionToExecute));
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
		public Task<string> SendKeySequenceAndGetSpokenTextAsync(params Key[] keys)
		{
			if (keys is null)
			{
				throw new ArgumentNullException(nameof(keys));
			}

			return Track<string>(() => SendKeySequenceAndGetSpokenTextInternalAsync(keys));
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

			_logger.LogInformation("Executing SendKeyCombinationsAsync. Combinations: " +
				string.Join(", ", combinations.Select(c => "(" + c.GetDescription() + ")")) +
				".");

			return Track(() => SendCombinationsInternalAsync(combinations));
		}

		/// <summary>
		/// Sends key combinations to NVDA and get spoken text asynchronously.
		/// </summary>
		/// <param name="combinations">The combinations.</param>
		/// <returns>The text spoken by NVDA after sending the key combinations</returns>
		public Task<string> SendKeyCombinationsAndGetSpokenTextAsync(params KeyCombination[] combinations)
		{
			if (combinations == null)
			{
				throw new ArgumentNullException(nameof(combinations));
			}

			_logger.LogInformation("Executing SendKeyCombinationsAndGetSpokenTextAsync. Combinations: " +
						 string.Join(", ", combinations.Select(c => "(" + c.GetDescription() + ")")) +
						 ".");

			return Track(() => SendKeyCombinationsAndGetSpokenTextInternalAsync(combinations));
		}

		/// <summary>
		/// Sends a keystroke sequence  to NVDA asynchronously.
		/// </summary>
		/// <param name="keys">The keys.</param>
		/// <returns>The task associated to this operation</returns>
		public Task SendKeysAsync(params Key[] keys)
		{
			if (keys == null)
			{
				throw new ArgumentNullException(nameof(keys));
			}

			_logger.LogInformation("Executing SendKeysAsync. Keys: " + string.Join("+", keys.Select(k => k.Name)) + ".");
			return Track(() => SendKeysInternalAsync(keys));
		}

		/// <summary>
		/// Sends the keys to NVDA (as combination) and get spoken text asynchronously.
		/// </summary>
		/// <param name="keys">The keys.</param>
		/// <returns>The text spoken by NVDA after sending the key combination</returns>
		public Task<string> SendKeysAndGetSpokenTextAsync(params Key[] keys)
		{
			if (keys == null)
			{
				throw new ArgumentNullException(nameof(keys));
			}

			_logger.LogInformation("Executing SendKeysAsync. Keys: " + string.Join("+", keys.Select(k => k.Name)) + ".");

			return Track(() => SendKeysAndGetSpokenTextInternalAsync(keys));
		}

		/// <summary>
		/// Send a command to NVDA without waiting for a response asynchronously.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>The task associated to this operation</returns>
		public Task SendCommandAsync(INvdaCommand command)
		{
			if (command == null)
			{
				throw new ArgumentNullException(nameof(command));
			}

			_logger.LogInformation($"Executing SendCommandAsync. Command: {command.GetDescription()}.");

			var combinationSet = GetKeyCombinationSet(command);
			return Track(() => SendKeyCombinationSet(combinationSet));
		}

		/// <summary>
		/// Sends a command to NVDA and waits for a response that will be returned by the method asynchronously.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>
		/// The text sopen by NVDA after sending the command
		/// </returns>
		/// <exception cref="ArgumentNullException">command</exception>
		public Task<string> SendCommandAndGetSpokenTextAsync(INvdaCommand command)
		{
			if (command == null)
			{
				throw new ArgumentNullException(nameof(command));
			}

			_logger.LogInformation($"Executing SendCommandAndGetSpokenTextAsync. Command: {command.GetDescription()}.");

			var combinationSet = GetKeyCombinationSet(command);
			return Track(() => SendKeyCombinationSetAndGetSpokenTextInternal(combinationSet));
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
			base.Dispose();
		}
		protected override async Task FinishDisposeAsync()
		{
			await DisconnectAsync();
			if (_internalLoggerFactory)
			{
				_loggerFactory.Dispose();
			}
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
			if (options?.LoggerFactory != null)
			{
				_loggerFactory = options.LoggerFactory;
			}
			else if (!options.DisableLogging)
			{
				_loggerFactory = new LoggerFactory();
				_loggerFactory.AddConsole()
					.AddDebug();
				_internalLoggerFactory = true;
			}

			_logger = _loggerFactory.CreateLogger("NvdaTestingDriver");
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
					_logger.LogDebug("This is the first time this NVDA version is started on this machine.");
					return ExtractNvdaZipFileInTmpDir();
				}

				string nvdaExecutableFilePath = (string)registryKey.GetValue(GetNvdaVersion());
				if (string.IsNullOrWhiteSpace(nvdaExecutableFilePath))
				{
					_logger.LogDebug("This is the first time this NVDA version is started on this machine.");
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
			_logger.LogDebug("Starting integrity checks...");
			string nvdaDirectory = Path.GetDirectoryName(nvdaExecutableFilePath);
			using (var nvdaZipStream = this.GetType().Assembly.GetManifestResourceStream("NvdaTestingDriver.nvda.zip"))
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
							_logger.LogDebug($"Looks like there's some deleted files. Restoring {nonExistingEntries.Count()} files...");
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
			string nvdaVersion = GetNvdaVersion();

			using (var nvdaZipStream = this.GetType().Assembly.GetManifestResourceStream("NvdaTestingDriver.nvda.zip"))
			{
				using (var zipArchive = new ZipArchive(nvdaZipStream))
				{
					var tmpDir = Path.Combine(Path.GetTempPath(), nvdaVersion);
					nvdaExecutableFilePath = Path.Combine(tmpDir, "nvda.exe");
					_logger.LogDebug("Extracting NVDA zip file. This may take a few seconds...");
					zipArchive.ExtractToDirectory(tmpDir);
					_logger.LogDebug("Zip extracted.");
					using (var registryKey = Registry.CurrentUser.CreateSubKey(NvdaRegistryKey, true))
					{
						registryKey.SetValue(nvdaVersion, nvdaExecutableFilePath, RegistryValueKind.String);
						_logger.LogDebug($"REgistry key adjusted to Nvda Version {nvdaVersion}, to executable path {nvdaExecutableFilePath}.");
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
			return $"nvda_{this.GetType().Assembly.GetName().Version}";
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

			bool socketAvailable = false;
			DateTime operationStart = DateTime.Now;
			do
			{
				try
				{
					await _tcpClient.ConnectAsync(host, port);
					_logger.LogDebug("TCP client connected.");
					socketAvailable = true;
				}
				catch (SocketException ex)
				{
					if ((DateTime.Now - operationStart) > timeout)
					{
						_logger.LogError($"Timeout exception while connecting to NVDA remote server. {ex.GetType()}: {ex.Message}.");
						throw;
					}

					_logger.LogWarning($"Retrying TCP connection to Nvda remote server. Previous attempt raised the exception: {ex.GetType()}: {ex.Message}.");
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

			_logger.LogDebug($"Sending message: \"{message}\"");
			byte[] buffer = Encoding.UTF8.GetBytes(message.ToCharArray(), 0, message.Length);
			await _sslStream.WriteAsync(buffer, 0, buffer.Length, _cancellationTokenSource.Token);
		}

		/// <summary>
		/// Starts the thread that will read incoming messages from the socket connected to NVDA Remote, allowing it to execute events dependent on the arrival of new messages.
		/// </summary>
		private void BeginReadMessages()
		{
			_taskReceivingMessages = Task.Run(async () =>
			{
				while (!_cancellationTokenSource.Token.IsCancellationRequested)
				{
					byte[] buffer = new byte[1024];
					StringBuilder messageData = new StringBuilder();
					int bytes = -1;
					do
					{
						if (_cancellationTokenSource.Token.IsCancellationRequested)
						{
							return;
						}

						// Read the client's test message.
						bytes = await _sslStream.ReadAsync(buffer, 0, buffer.Length, _cancellationTokenSource.Token);

						// Use Decoder class to convert from bytes to UTF8
						// in case a character spans two buffers.
						Decoder decoder = Encoding.UTF8.GetDecoder();
						char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
						decoder.GetChars(buffer, 0, bytes, chars, 0);
						messageData.Append(chars);
					}
					while (bytes == buffer.Length);
					string message = messageData.ToString();
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
			if (messages == null)
			{
				return;
			}

			if (messages.Type == JTokenType.Object && messages["type"].Value<string>() == "cancel")
			{
				this.OnSpeakCancelled?.Invoke(this, EventArgs.Empty);
				return;
			}

			StringBuilder finalMessage = new StringBuilder();
			foreach (var token in messages)
			{
				if (token.Type == JTokenType.String)
				{
					finalMessage.AppendLine(SanitizeNvdaSpokenMessage(token.Value<string>()));
				}
			}

			if (finalMessage.Length > 0)
			{
				string processedMessage = finalMessage.ToString();
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
		/// <returns>The task that will contain the spoken message</returns>
		private async Task<string> SendKeyCombinationSetAndGetSpokenTextInternal(List<KeyCombination> combinationSet)
		{
			await StopReadingAsync();
			return await GetNextSpokenMessageAsync(async () =>
			{
				foreach (var combination in combinationSet.Where(c => c.Any()))
				{
					await SendKeysAsync(combination.ToArray());
				}
			});
		}

		/// <summary>
		/// Sends the key combinations and get spoken text asynchronously (internal use).
		/// </summary>
		/// <param name="combinations">The combinations.</param>
		/// <returns>The text spoken by NVDA after sending the key combinations</returns>
		private async Task<string> SendKeyCombinationsAndGetSpokenTextInternalAsync(KeyCombination[] combinations)
		{
			await StopReadingAsync();
			return await GetNextSpokenMessageAsync(async () =>
			{
				await SendKeyCombinationsAsync(combinations);
			});
		}

		/// <summary>
		/// Sends a set of key combinations to NVDA.
		/// </summary>
		/// <param name="combinationSet">The combination set.</param>
		/// <returns>The task associated to this operation</returns>
		private async Task SendKeyCombinationSet(List<KeyCombination> combinationSet)
		{
			foreach (var combination in combinationSet.Where(c => c.Any()))
			{
				await SendKeysAsync(combination.ToArray());
			}
		}

		/// <summary>
		/// Sends the key combinations asynchronousl (internal use).
		/// </summary>
		/// <param name="combinations">The combinations.</param>
		/// <returns>The task associated to this operation</returns>
		private async Task SendCombinationsInternalAsync(KeyCombination[] combinations)
		{
			foreach (var combination in combinations)
			{
				await SendKeysAsync(combination.ToArray());
			}
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
				_logger.LogDebug($"Sending key Down: {k.Name}.");
				await WriteMessageAsync(ToJsonKey(k, true));
			}

			foreach (var k in keys.Reverse())
			{
				_logger.LogDebug($"Sending key up: {k.Name}.");
				await WriteMessageAsync(ToJsonKey(k, false));
			}
		}

		/// <summary>
		/// Sends the keys to NVDA and gets the spoken text asynchronously.
		/// </summary>
		/// <param name="keys">The keys.</param>
		/// <returns>The text spoken by NVDA after sending keys</returns>
		private async Task<string> SendKeysAndGetSpokenTextInternalAsync(Key[] keys)
		{
			await StopReadingAsync();
			return await GetNextSpokenMessageAsync(async () =>
			{
				await SendKeysAsync(keys);
			});
		}

		/// <summary>
		/// Connects the driver asynchronously against a portable NVDA instance that will start automatically, configured with the options provided in the driver constructor.
		/// </summary>
		/// <returns>The task associated to this operation.</returns>
		/// <exception cref="NvdaTestingDriver.Exceptions.AlreadyConnectedException">Throws if the method has been called before, and the driver is already connected</exception>
		private async Task ConnectInternalAsync()
		{
			_logger.LogInformation("Starting NVDA connection.");
			if (_tcpClient != null && _tcpClient.Connected)
			{
				_logger.LogError("The driver is already connected.");
				throw new AlreadyConnectedException();
			}

			var nvdaExecutableFilePath = GetNvdaExecutableFilePath();
			var nvdaDirectory = Path.GetDirectoryName(nvdaExecutableFilePath);
			var nvdaDriverOptionsWriter = new NvdaDriverOptionsWriter(_nvdaDriverOptions);
			_logger.LogDebug("Writing options into NVDA ini file...");
			nvdaDriverOptionsWriter.WriteOptionsToIniFile(Path.Combine(nvdaDirectory, "userConfig", "nvda.ini"));
			_logger.LogDebug("Killing previous NVDA process to avoid multi-instance problems.");
			KillPreviousNVDA();
			_logger.LogDebug("Starting NVDA process...");
			ProcessStartInfo processParam = new ProcessStartInfo { FileName = nvdaExecutableFilePath };
			_nvdaProcess = Process.Start(processParam);
			_nvdaProcess.WaitForInputIdle();
			_logger.LogDebug("Nvda estarted.");
			_logger.LogDebug("Starting TCP connection to Nvda Remote Server...");
			_tcpClient = new TcpClient();
			await ConnectSocketAsync(LocalHost, NvdaRemotePort);
			_networkStream = _tcpClient.GetStream();
			_sslStream = new SslStream(_networkStream, false, (sender, certificate, chain, sslPolicyErrors) => true, null);
			_logger.LogDebug("Authenticating SSL connection...");
			await _sslStream.AuthenticateAsClientAsync("nvdaremote.com");
			_cancellationTokenSource = new CancellationTokenSource();
			_logger.LogDebug("Start reading messages from remote server.");
			BeginReadMessages();
			await WriteMessageAsync(SetCommunicationProtocolMsg);
			await WriteMessageAsync(SetConnectionMsg);
		}

		/// <summary>
		/// Disconnects the driver asynchronously, shutting down the NVDA instance.
		/// </summary>
		/// <returns>The task associated to this operation.</returns>
		private async Task DisconnectInternalAsync()
		{
			_logger.LogInformation("Disconnecting NvdaTetingDriver...");
			if (_sslStream != null && _sslStream.CanWrite)
			{
				await ShutdownNvdaAsync();
			}

			_cancellationTokenSource?.Cancel();
			_cancellationTokenSource.Dispose();

			if (_taskReceivingMessages != null)
			{
				await Task.WhenAll(_taskReceivingMessages);
			}

			_tcpClient?.Close();
			_tcpClient?.Dispose();
			_sslStream?.Dispose();
			_networkStream?.Dispose();
			_nvdaProcess?.WaitForExit(10000);
			try
			{
				_nvdaProcess?.Kill();
			}
			catch (Exception ex) when (ex is InvalidOperationException || ex is Win32Exception)
			{
			}

			_nvdaProcess.Dispose();
		}

		/// <summary>
		/// Gets the next spoken message by NVDA asynchronously.
		/// </summary>
		/// <param name="actionToExecute">The action to execute, which should cause NVDA to return
		/// some message, which will be returned by this method.</param>
		/// <returns>The task that once completed, will contain The NVDA spoken text immediately following the execution of the action.</returns>
		private Task<string> GetNextSpokenMessageInternalAsync(Func<Task> actionToExecute) =>
				  GetNextSpokenMessageAsync(null, null, actionToExecute);

		/// <summary>
		/// Gets the next spoken message by NVDA asynchronous.
		/// </summary>
		/// <param name="timeout">The maximum time the method will wait for an NVDA message before raising an exception. By default, timeout is 3 seconds.</param>
		/// <param name="timeToWaitNewMessages">
		/// There are occasions when NVDA transmits messages separately, in different packets.
		/// This parameter sets the maximum time that NVDA will concatenate new messages received within the return of this method, starting from the arrival of the first message.</param>
		/// <param name="actionToExecute">The action to execute, which should cause NVDA to return
		/// some message, which will be returned by this method.</param>
		/// <returns>The task that once completed, will contain The NVDA spoken text immediately following the execution of the action.</returns>
		/// <exception cref="NvdaTestingDriver.Exceptions.TimeoutException">Timeout exceeded when trying to get the next message transmitted by NVDA.</exception>
		private async Task<string> GetNextSpokenMessageInternalAsync(TimeSpan? timeout = null, TimeSpan? timeToWaitNewMessages = null, Func<Task> actionToExecute = null)
		{
			_logger.LogInformation("Executting: GetNextSpokenMessageAsync.");

			if (timeout == null)
			{
				timeout = TimeSpan.FromSeconds(3);
			}

			if (timeToWaitNewMessages == null)
			{
				timeToWaitNewMessages = TimeSpan.FromMilliseconds(300);
			}

			CheckConnectivity();

			StringBuilder spokenMessage = new StringBuilder();
			DateTime? firstMessageTime = null;

			void LocalSpeakReceived(object sender, string message)
			{
				_logger.LogDebug($"Event received: OnSpeakReceived. Message: \"{message}\"");
				if (!string.IsNullOrWhiteSpace(message))
				{
					if (spokenMessage.Length == 0)
					{
						firstMessageTime = DateTime.Now;
					}

					spokenMessage.Append((spokenMessage.Length > 0 ? Environment.NewLine : string.Empty) + message);
				}
			}

			this.OnSpeakReceived += LocalSpeakReceived;
			if (actionToExecute != null)
			{
				_logger.LogDebug("Executing action before receiving speak messages...");
				await actionToExecute.Invoke();
				_logger.LogDebug("Action executed.");
			}

			var operationStart = DateTime.Now;
			while ((spokenMessage.Length == 0 || (firstMessageTime.HasValue && DateTime.Now - firstMessageTime.Value < timeToWaitNewMessages))
				&& DateTime.Now - operationStart < timeout)
			{
				await Task.Delay(100);
			}

			this.OnSpeakReceived -= LocalSpeakReceived;
			if (spokenMessage.Length == 0 && DateTime.Now - operationStart > timeout)
			{
				_logger.LogError($"Error receiving spoken message. The defined timeout was exceeded: {timeout.Value.TotalMilliseconds} milliseconds.");
				throw new Exceptions.TimeoutException("Timeout exceeded when trying to get the next message transmitted by NVDA.");
			}

			_logger.LogInformation($"Final spoken message: \"{spokenMessage}\"");
			return spokenMessage.ToString();
		}

		/// <summary>
		/// Sends a key sequence to NVDA asynchronously.
		/// </summary>
		/// <param name="keys">The keys.</param>
		/// <returns>The task associated to this operation</returns>
		private async Task SendKeySequenceInternalAsync(params Key[] keys)
		{
			_logger.LogInformation("Executing SendKeySequenceAsync. Keys: " +
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
		/// <returns>
		/// Te text spoken by NVDA after sending the key sequence
		/// </returns>
		private async Task<string> SendKeySequenceAndGetSpokenTextInternalAsync(params Key[] keys)
		{
			_logger.LogInformation("Executing SendKeySequenceAndGetSpokenText. Keys: " +
				string.Join(", ", keys.Select(k => k.Name)) + ".");

			await StopReadingAsync();
			return await GetNextSpokenMessageAsync(async () =>
			{
				await SendKeySequenceAsync(keys);
			});
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
			_logger.LogInformation("Executing StopReadingAsync.");

			if (timeout == null)
			{
				timeout = TimeSpan.FromMilliseconds(500);
			}

			bool cancelReceived = false;
			void ONLocalSpeakCancelled(object sender, EventArgs e)
			{
				_logger.LogDebug("Event received: OnSpeakCancelled.");
				cancelReceived = true;
			}

			this.OnSpeakCancelled += ONLocalSpeakCancelled;
			var operationStart = DateTime.Now;
			await SendKeysAsync(Key.Control);
			while (!cancelReceived && (DateTime.Now - operationStart) < timeout)
			{
				_logger.LogDebug("Sending control (we don't received cancelled signal yet...");
				await Task.Delay(100);
			}

			if ((DateTime.Now - operationStart) > timeout)
			{
				throw new Exceptions.TimeoutException("Timeout while waiting for stopping speech.");
			}

			this.OnSpeakCancelled -= ONLocalSpeakCancelled;
		}

		/// <summary>
		/// Shutdowns the nvda instance, by sending a Nvda+Q keystroke.
		/// </summary>
		/// <returns>The task associated to this operation</returns>
		private Task ShutdownNvdaAsync()
		{
			return SendCommandAsync(BasicCommands.QuitNvda);
		}
	}
}
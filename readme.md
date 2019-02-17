# Nvda Testing Driver: Automate your tests with a screen reader

## Introduction. What is NvdaTestingDriver?

NvdaTestingDriver is a package that will allow us to perform functional tests of accessibility, using the screen reader NVDA.
The package is able to start the screen reader (it does not need installation, as it already includes a portable version), and handle it as if it were the user himself who interacts with it, sending it commands and receiving the textual responses. With these responses, we will be able to prepare our tests, which will fail if the texts received do not correspond to what we consider the reader should pronounce at all times.

NvdaTestingDriver, although it is a NetStandard package, is only compatible with Windows (Vista versions onwards), as NVDA is a screen reader developed for this operating system.

In this repository you will be able to download the source code, as well as a webt project and a test project, which will use the example web to execute the functional tests.

## What is a screen reader?

A screen reader is a software that is capable of identifying and interpreting what appears on a computer screen, and transferring it to the user by means of a voice synthesis or a Braille screen. Thus, this software allows a person who is blind or has low vision to operate a computer by being able to read what is happening on the screen at all times.

But for the screen reader to be able to read an application or website, it has to be accessible.

Starting from this premise, NVDA is a wonderful tool to test if our website is accessible or not, since the reader must be able to convert all our website to text that a blind or low vision person can read: images, site structure, forms ...

## How does Nvda Testing Driver work?

Nvda Testing Driver will allow us to run the NVDA screen reader automatically, manage it from .Net, obtain the textual responses that the screen reader would announce aloud, and compare those answers with the texts that we expect the reader to pronounce, if the component to be tested works as expected.

So, for example, if we have a contact page, and we know that in the name edit box, NVDA should read: "Name: text edit", we could create a test that navigates to the contact page, positions itself over the edit box, gets the text that NVDA will return when positioned over it, and compares it with what we know it should pronounce. If the texts match, the component is read as expected, and the test passes. Otherwise, something has failed, the texts don't match, and the test will fail.


This example is very simple, but imagine a more complex component, such as a tree view that we have programmed with Aria. If we manually check that this tree view works well with NVDA, we can create a test that positions itself over it, navigate expanding and collapsing nodes, and compare the texts returned by NVDA with what we know it should pronounce.
So, if at any time during web development the tree view stops working properly, the automatic execution of the test will allow us to realize quickly, detect the bug and correct it, without having to manually check all pages to ensure that they still maintain a good level of accessibility with a screen reader.
In one of the example tests you have a test that interacts with a tree view and guarantees that NVDA works correctly with it.

## How do I get the texts that NVDA should pronounce to build my tests?

NVDA has a great tool called "Voice viewer". This tool will keep a log of everything NVDA pronounces during a session. So, to build our test, all we have to do is start the screen reader, open the "Voice Viewer" tool, navigate to the website, interact with the elements we want to test, and then copy the log of everything pronounced by NVDA and build our tests according to those texts.

## NVDA commands

NVDA has a large number of orders to execute a multitude of actions. To facilitate interaction and prevent coding errors, this library includes, as already defined commands, the most relevant NVDA functions, written in several static classes within the `NvdaTestingDriver.Commands.NvdaCommands` namespace. Each class contains a category, as we can see in the [NVDA help](https://www.nvaccess.org/files/nvda/documentation/userGuide.html?).

## Execution flow

### Instantiating a `NvdaTestingDriver` object

The first thing we have to do is instantiate the `NvdaTestingDriver` class. We can do this instantiation without parameters (NVDA will be loaded with the default options), or passing it a function that will receive as parameter the object `NvdaOptions` , which we can change to modify the default behavior of the screen reader. For example:

```csharp
			var nvdaDriver = new NvdaDriver(opt =>
			{
				opt.GeneralSettings.Language = NvdaTestingDriver.Settings.NvdaLanguage.English;
			});

```

### Start NVDA

The next step is to connect the driver, i.e. start NVDA and connect to it to control it.

```csharp
await nvdaTestingDriver.ConnectAsync();
```

### Interacting with NVDA

The third step is to interact with NVDA. 

This library contains several methods for sending commands to NVDA and receiving textual responses from the screen reader. With the NVDA response, we will be able to make comparisons with the texts we expect to receive, and thus determine if any component is not behaving as it should.

**Important**: Remember that to make comparisons you must use the `TextContains` method of the NvdaTestHelper class, or, if you want the method to raise an AssertFailedException , use the `TextContains` method of the `NvdaAssert` class (NvdaTestingDriver.MSTest package).

Here we explain the most important methods:

#### `SendCommandAndGetSpokenTextAsync`

This method will receive a command (it can be predefined or built manually indicating the combinations of keys to send), and will return the response that NVDA will verbalize as a result of that command.

For example:

```csharp
string text = await nvdaDriver.SendCommandAndGetSpokenTextAsync(BrowseModeCommands.NextFormField);
```

This instruction will send the command `BrowseModeCommands.NextFormField` to NVDA (equivalent to sending the keystroke `F`, it will wait for NVDA to send a response, and it will return that response, which will be stored in the `text` variable. We can use that variable to make the necessary comparisons and make sure that the command responds as expected.


#### `SendKeysAndGetSpokenTextAsync`

This method will receive a list of keys (they will be treated as a combination), and will return the response that NVDA will verbalize as a result of those keystrokes.

For example:

```csharp
string text = await nvdaDriver.SendKeysAndGetSpokenTextAsync(Key.DownArrow);
```

This instruction will send the `DownArrow` key to NVDA, wait for NVDA to send a response, and return that response, which will be stored in the `text` variable. We can use that variable to make the necessary comparisons and make sure that the command responds as expected.

### Disconnect and exit NVDA

The last step is to close the NVDA connection and quit the screen reader:

```csharp
			await NvdaDriver.DisconnectAsync();
			```

This command will disconnect the remote control to NVDA, and then exit the screen reader.

Since `NvdaTestingDriver` implements the `IDisposable` interface, if we include the instantiation of the class within a `using` block, when we close the block, the `Dispose` method will be executed, and it will call the `DisconnectAsync` method, closing the remote control and exiting NVDA.

## Packages included in this repository

There are three nuget packages (with their corresponding projects) included in this repository:
- NvdaTestingDriver: the main package, which handles all interaction with NVDA.
- NvdaTestingDriver.MSTest: Includes methods that help the execution of tests with the MSTests engine.
- NvdaTestingDriver.Selenium: Includes extensions for IWebDriver (Selenium), such as Focus (to focus on any element), or FocusWindow (to place the focus inside the web document window).

In the example project `AccessibleDemo.Tests`, you will see how these three packages are used together.

## Compatibility

These packages are compatible with NetStandard 2.0. Therefore, you can use them from .Net Framework 4.6.1 or higher, and .NetCore 2.0 or higher. I have tried to make them compatible with .Net Core 1.0 to 2.0, but there are Process functions that were not developed for those versions.

## Creating our tests

Imagine we want to verify that the Github buttons are accessible.

1. Create a new project like *MSTest Test Project (.NET Core)*.
2. Give it a name and a location.
3. Install the following packages:
    - NvdaTestingDriver
    - NvdaTestingDriver.MSTest
    - Selenium.WebDriver 
    - Selenium.Chrome.WebDriver 
    - NvdaTestingDriver.Selenium
4. Download the [NVDA screen reader](https://www.nvaccess.org/download/)
5. Install NVDA, or create a portable copy (the installer will give you that option).
6. Access to Github using Chrome. I have used the DotNetCore repository as an example: [https://github.com/dotnet/core](https://github.com/dotnet/core).
7. Start NVDA. The first time, the welcome dialog will appear. Choose the keyboard layout that suits you best and click OK.
8. Open the NVDA menu by pressing Insert + N. Go to *Tools* and click on *Voice viewer*.
9. A window will open in which everything that NVDA will be pronouncing will appear.
10. Go back to Github's window without closing the *voice viewer* window. Once in the window, press the *D* key to move between regions of the page. Press the key four times until NVDA is positioned in the *Repository Navigation* region, and then press the *B* key twice until NVDA is positioned over the button to choose the branch. NVDA will say something like: `button collapsed subMenu Branch: master`. At the moment, NVDA is on the button, but the system focus is not, as NVDA uses a virtual buffer to navigate between controls. To force the focus to be positioned on the current element of the virtual buffer, press twice *insert+shift+numpadMinus* (if you chose the desktop keyboard layout), or *shift+insert+backspace* (if you chose the laptop layout).
11. Press the command for NVDA to read the element that has the focus (insert+tab). NVDA will read something like: `Branch: master button focused collapsed subMenu Switch branches or tags`.
12. Go back to the *voice viewer* window. You will see that in one of the last lines, will appear the text that NVDA pronounced when we press the command insert+tab: `Branch: master button focused collapsed subMenu Switch branches or tags`. Copy that text.
13. Close NVDA by pressing `Insert + Q`.
14. Go to the *Visual Studio* window, and in the project, create a new class called: `TestHelper`.
15. In the class, paste the following code:

        using System;
        using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
        using NvdaTestingDriver;
        using NvdaTestingDriver.Selenium;
        using OpenQA.Selenium;
        using OpenQA.Selenium.Chrome;
        
        namespace RemoteWebsites.Tests
        {
        	[TestClass]
        	public static class TestHelper
        	{
        
        		internal static WebDriverWrapper WebDriverWrapper = new WebDriverWrapper();
        
        		internal static IWebDriver WebDriver { get; private set; }
        
        
        		internal static NvdaDriver NvdaDriver;
        
        		/// <summary>
        		/// This method will be executed before starting any test
        		/// </summary>
        		/// <param name="context">The context.</param>
        		/// <returns>The task associated to this operation</returns>
        		[AssemblyInitialize]
        		public static async Task Initialize(TestContext context)
        		{
        
        			// Initialize the Selenium WebDriveer
        			UpWebDriver();
        
        			// Starts the NVDATestingDriver
        			await ConnectNvdaDriverAsync();
        		}
        
        		private static void UpWebDriver()
        		{
        			try
        			{
        
        				// We started the WebDriver using the UpWebDriver method of the WebDriverWrapper class,
        				// to manage the chrome window, and get to put it in the foreground when necessary.
        				WebDriver = WebDriverWrapper.UpWebDriver(() =>
        				{
        					var op = new ChromeOptions
        					{
        						AcceptInsecureCertificates = false
        					};
        					var webDriver = new ChromeDriver(Environment.CurrentDirectory, op);
        					webDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromMinutes(3);
        					webDriver.Manage().Window.Maximize();
        					return webDriver;
        				});
        			}
        			catch (Exception ex)
        			{
        				Console.WriteLine($"Error while starting Chrome WebDriver: {ex.Message}");
        				throw;
        			}
        		}
        
        
        		/// <summary>
        		/// Connects the nvda driver asynchronously.
        		/// </summary>
        		/// <returns></returns>
        		private static async Task ConnectNvdaDriverAsync()
        		{
        			try
        			{
        				// We start the NvdaTestingDriver:
        				NvdaDriver = new NvdaDriver(opt =>
        				{
        					opt.GeneralSettings.Language = NvdaTestingDriver.Settings.GeneralSettings.NvdaLanguage.English;
        				});
        				await NvdaDriver.ConnectAsync();
        			}
        			catch (Exception ex)
        			{
        				Console.WriteLine($"Error while starting NVDA driver: {ex.Message}");
        				throw;
        			}
        		}
        
        		/// <summary>
        		/// This method will be executed when all e tests are finished.
        		/// </summary>
        		/// <returns></returns>
        		[AssemblyCleanup]
        		public static async Task Cleanup()
        		{
        			try
        			{
        				WebDriver.Quit();
        			}
        			catch
        			{
        				// If the web  driver quit fails, we continue.
        			}
        
        			try
        			{
        				WebDriver.Dispose();
        			}
        			catch
        			{
        				// If the web  driver dispose fails, we continue.
        			}
        
        			try
        			{
        				await NvdaDriver.DisconnectAsync();
        			}
        			catch
        			{
        				// if disconnect fails, we continue.
        			}
        		}
        	}
        }

16. Create a second class, where we will store our test methods:


        using System.Threading.Tasks;
        using Microsoft.VisualStudio.TestTools.UnitTesting;
        using NvdaTestingDriver.Commands.NvdaCommands;
        using NvdaTestingDriver.MSTest;
        using NvdaTestingDriver.Selenium.Extensions;
        using OpenQA.Selenium;

        namespace RemoteWebsites.Tests
        {
        	[TestClass]
        	public class GithubRepoPageShould
        	{
        
        		[TestMethod]
        		public async Task CheckDownloadButtonIsCollapsibleAndExpandibleAsync()
        		{
        			
        			// Arrange:
			        // We tell the WebDriverWrapper to put the Chrome window in the foreground.
        			// NVDA needs the window to be in the foreground in order to interact with that window.
        			TestHelper.WebDriverWrapper.SetBrowserWindowForeground();
        
        			// Go to dotnet core github repository:
        			TestHelper.WebDriver.Navigate().GoToUrl("https://github.com/dotnet/core");
					// We put the focus on chrome window:
        			TestHelper.WebDriver.FocusOnWindow();
        
        			// We put the focus inside the first summary tag with btn class:
        			TestHelper.WebDriver.Focus(TestHelper.WebDriver.FindElement(By.CssSelector("summary.btn")));
        
        			// Act & asserts
        			// We send the ReportCurrentFocus command to NVDA and get the text:
        			string text = await TestHelper.NvdaDriver.SendCommandAndGetSpokenTextAsync(NvdaTestingDriver.Commands.NvdaCommands.NavigatingSystemFocusCommands.ReportCurrentFocus);
        
        			// We use the NvdaAssert.TextContains method to check that the text pronounced by NVDA
        			// contains the text it should say.
        			// This method sanitize both the expected text and the received text, to remove spaces, line breaks and other characters that could affect the result.
        			// Whenever you want to compare text with NVDA,
        			// use either the TextContains method of the NvdaAsert class (NvdaTestingDriver.MSTest package),
        			// which will throw an AssertFailedException if the text specified is not present in the
        			// text pronounced by NVDA, or the method TextContains of the NvdaTestHelper class
        			// (NvdaTestingDriver package), which will return true or false.
        			NvdaAssert.TextContains(text, "Branch: master button focused collapsed sub Menu Switch branches or tags");
        		}
        
        	}
        }

17. Compile the application and run the newly created test. *Important*: If you are using a screen reader, you must deactivate it before running the tests, as this screen reader will interfere with the NVDA that starts automatically with the test.
18. As long as the chosen button continues to maintain the same behavior, this test should pass.


## Other examples

In this repository you can find, within the `NvdaTestingDriver` solution, a folder called `Examples`, and within it, two examples. The first example (`RemoteWebsites.Tests`) is the one shown above, on how to check that a certain Github button is accessible.
The second one (`AccessibleDemo.Tests`), is based on the `AccessibleDemo` project, also included in this folder. In this example there are several tests that will check the interaction with different components within that web.


## Integrating `NvdaTestingDriver` into a continuous integration flow

It is possible to integrate tests using `NvdaTestingDriver` within a continuous integration flow. I have tested it using Azure DevOps pipelines, but you have to take some things into account:

- You can only use your own agents, not the self-hosted agents in Azure. This is because self-hosted agents use the non-interactive mode, which means that even if `Selenium` opens a web page, that web will never be visible to NVDA.
- When installing our own agent in *Azure Devops*, we must make sure that we choose the interactive mode, which will allow the agent to interact with the Windows interface, and thus, NVDA will be able to read any window opened by our tests.

In conclusion: it is possible to create regression tests on our website with NVDA, create required policies to push code into a branch, add the execution of our tests in the build of that policy, and if any test does not pass, reject the pull request. Wonderful! ;)


## Collaborate with `NvdaTestingDriver`

If you want to collaborate with this project, any contribution will be welcome! Send me all the pull requests you consider, and let's make this project grow with the help of all of you!

## Acknowledgements

This project would not have been possible without all the people who have helped me with some aspect of it:

- To [Pablo Núñez](https://www.twitter.com/pablonete), who in a Twitter conversation gave me the idea of doing screen reader tests, and was the seed of this project. Hopefully you can use it in your developments! :)
- To [Christopher Toth](https://www.twitter.com/mongoose_q) and Tyler Spivey, who developed the wonderful complement for NVDA [NvdaRemote](https://www.nvdaremote.com), and that has allowed me to control NVDA remotely.
- To [Jose Manuel Delicado](https://www.twitter.com/jmdaweb), who helped me to understand a little better the NvdaRemote plugin and to modify it to be able to debug it.
- And last but not least, to my wife, [Núria](https://www.twitter.com/amaterasu_n), who puts up with my hours sitting in front of the computer, and takes care of everything during those hours in which I am immersed in this world of codes. Thank you, my love!

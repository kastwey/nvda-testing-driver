using System.Diagnostics;
using NvdaTestingDriver;
using NvdaTestingDriver.HttpApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

NvdaDriver nvdaDriver = await NvdaDriverHelper.InitializeAsync();

// Configure the HTTP request pipeline.

// TODO: This endpoint is currently an example of NvdaDriver being called from the API, not a final thing ;)
app.MapGet("/command", async () =>
{
	Debug.WriteLine($"/command");

	System.TimeSpan timeout = System.TimeSpan.FromSeconds(60);
	NvdaTestingDriver.Commands.NvdaCommand command = NvdaTestingDriver.Commands.NvdaCommands.NavigatingSystemFocusCommands.ReportCurrentFocus;

	string text = await nvdaDriver.SendCommandAndGetSpokenTextAsync(command, timeout);
	Debug.WriteLine(text);

	return text;
});

// Yet another example
app.MapGet("/healthcheck", () =>
{
	Debug.WriteLine($"/healthcheck");
	return "OK";
});

app.Run();

// TODO: This is not being called if the app is killed;
// it should be part of the server lifecycle, shouldn't it?
// I guess there should be something like  app.onStop(NvdaDriverLifecycle.CleanupAsync)
await NvdaDriverHelper.CleanupAsync();

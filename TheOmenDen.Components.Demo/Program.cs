using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.Bootstrap;
using Blazorise.LoadingIndicator;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Events;
using TheOmenDen.Components.Demo;
using TheOmenDen.Shared.Components.DependencyInjection;


try
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);

    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
        .Enrich.WithAssemblyInformationalVersion()
        .Enrich.WithEnvironmentName()
        .Enrich.WithEnvironmentUserName()
        .Enrich.WithMemoryUsage()
        .Enrich.WithProcessId()
        .Enrich.WithProcessName()
        .Enrich.WithThreadId()
        .Enrich.WithThreadName()
        .Enrich.WithDemystifiedStackTraces()
        .Enrich.WithSpan()
        .WriteTo.BrowserConsole()
        .WriteTo.Debug()
        .WriteTo.Console()
        .CreateLogger();

    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");

    builder.Logging
        .ClearProviders()
        .AddSerilog(Log.Logger, dispose: true);

    builder.Services.AddBlazorise(options => options.Immediate = true)
        .AddBootstrap5Components()
        .AddBootstrap5Providers()
        .AddBootstrapIcons()
        .AddLoadingIndicator();

    builder.Services.AddSingletonScriptLoader();

    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

    await builder.Build().RunAsync();

    Log.Information("Application started");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred while running the application");
}
finally
{
    Log.Information("Application shutting down");
    await Log.CloseAndFlushAsync();
}
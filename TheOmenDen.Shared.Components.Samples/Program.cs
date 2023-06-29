using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.Bootstrap;
using Blazorise.LoadingIndicator;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Serilog;
using Serilog.Events;
using TheOmenDen.Shared.Components.DependencyInjection;
using TheOmenDen.Shared.Components.Samples;

try
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
 
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .Enrich.WithThreadName()
        .Enrich.WithThreadId()
        .Enrich.WithProcessName()
        .Enrich.WithAssemblyVersion()
        .Enrich.WithAssemblyName()
        .Enrich.WithMemoryUsage()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.BrowserConsole()
        .CreateLogger();
    
    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");

    builder.Services.AddSingletonScriptLoader();

    builder.Services.AddBlazorise(options => options.Immediate = true)
        .AddBootstrap5Providers()
        .AddBootstrap5Components()
        .AddLoadingIndicator()
        .AddBootstrapIcons();


    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred while running the application");
}
finally
{
    await Log.CloseAndFlushAsync();
}
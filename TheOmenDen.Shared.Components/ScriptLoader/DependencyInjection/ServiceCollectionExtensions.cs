using Microsoft.Extensions.DependencyInjection;

namespace TheOmenDen.Shared.Components.ScriptLoader.DependencyInjection;
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the <see cref="IScriptLoaderService"/> as a scoped service.
    /// </summary>
    /// <param name="services"></param>
    /// <returns>The <see cref="IServiceCollection"/> <paramref name="services"/> for further chaining.</returns>
    public static IServiceCollection AddScriptLoader(this IServiceCollection services)
    {
        services.AddScoped<IScriptLoaderService, ScriptLoaderService>();
        return services;
    }

    /// <summary>
    /// Registers the <see cref="IScriptLoaderService"/> as a singleton service. This method should only be used if you are using a Blazor WebAssembly application.
    /// Usage in blazor server applications is not recommended - and can cause issues, unexpected behavior and potentially dangerous results.
    /// </summary>
    /// <param name="services"></param>
    /// <returns>The <see cref="IServiceCollection"/> <paramref name="services"/> for further chaining.</returns>
    public static IServiceCollection AddSingletonScriptLoader(this IServiceCollection services)
    {
        services.AddSingleton<IScriptLoaderService, ScriptLoaderService>();
        return services;
    }
}

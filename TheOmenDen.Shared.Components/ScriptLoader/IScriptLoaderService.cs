using TheOmenDen.Shared.Components.ScriptLoader.Options;

namespace TheOmenDen.Shared.Components.ScriptLoader;

/// <summary>
/// <para>A service responsible for loading scripts in a Blazor Application.</para>
/// <para>These scripts can be loaded either asynchronously or synchronously (in Blazor WebAssembly ONLY)</para>
/// <para>The asynchronous methods all have <see cref="CancellationToken"/> support, allowing them to be cancelled by the caller</para>
/// </summary>
internal interface IScriptLoaderService
{
    /// <summary>
    /// Allows you to load a script asynchronously in a Blazor Application
    /// </summary>
    /// <param name="url">The URL of the script you want to load, as a <see langword="string"/></param>
    /// <param name="options">The set of options to load the script with</param>
    /// <param name="cancellationToken">Provides cancellation support</param>
    /// <returns><see cref="Task"/></returns>
    Task LoadScriptAsync(string url, ScriptLoaderOptions options,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Allows you to load multiple scripts asynchronously in a Blazor Application
    /// </summary>
    /// <param name="urls">The provided URLS of the scripts you wish to load as <see langword="string"/></param>
    /// <param name="options">The options that you want to set FOR ALL the scripts</param>
    /// <param name="cancellationToken">Provides cancellation support</param>
    /// <returns><see cref="Task"/></returns>
    Task LoadScriptsAsync(IEnumerable<string> urls, ScriptLoaderOptions options,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Allows you to load a scrtipt synchronously in a Blazor WEBASSEMBLY Application ONLY.
    /// </summary>
    /// <param name="url">The URL of the script you want to load, as a <see langword="string"/></param>
    /// <param name="options">The set of options to load the script with</param>
    void LoadScript(string url, ScriptLoaderOptions options);
}
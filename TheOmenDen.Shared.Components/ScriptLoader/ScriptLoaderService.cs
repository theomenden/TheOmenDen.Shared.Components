using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using TheOmenDen.Shared.Components.ScriptLoader.Options;

namespace TheOmenDen.Shared.Components.ScriptLoader;

/// <inheritdoc cref="IScriptLoaderService"/>
internal sealed class ScriptLoaderService : IScriptLoaderService
{
    private const string ScriptLoader = "scriptLoader.loadScript";
    private readonly IJSRuntime _jsRuntime;
    private readonly IJSInProcessRuntime? _jsInProcessRuntime;
    private readonly ILogger<ScriptLoaderService> _logger;

    public ScriptLoaderService(IJSRuntime jsRuntime, ILogger<ScriptLoaderService> logger)
    {
        _jsRuntime = jsRuntime;
        _jsInProcessRuntime = jsRuntime as IJSInProcessRuntime;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task LoadScriptAsync(string url, ScriptLoaderOptions options,
        CancellationToken cancellationToken = default)
        => await LoadScriptsAsync(new List<string> { url }, options, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    public async Task LoadScriptsAsync(IEnumerable<string> urls, ScriptLoaderOptions options,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Attempting to Load {Count} scripts", urls.TryGetNonEnumeratedCount(out var count) ? count.ToString() : "a few");
        foreach (var url in urls)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogWarning("Script loading was cancelled");
                break;
            }

            if (isAbleToUseInProcessRuntime())
            {
                await _jsInProcessRuntime!
                    .InvokeVoidAsync(ScriptLoader, cancellationToken, url, options)
                    .ConfigureAwait(false);
                continue;
            }

            await _jsRuntime
                .InvokeVoidAsync(ScriptLoader, cancellationToken, url, options)
                .ConfigureAwait(false);
        }

        _logger.LogInformation("Scripts Loaded");
    }

    /// <inheritdoc/>
    public void LoadScript(string url, ScriptLoaderOptions options)
    {
        if (!isAbleToUseInProcessRuntime())
        {
            _logger.LogError("The current JSRuntime does not support synchronous operations. Synchronous script loading is only supported in Blazor WebAssembly");
            throw new NotSupportedException("The current JSRuntime does not support synchronous operations. Synchronous script loading is only supported in Blazor WebAssembly");
        }

        _jsInProcessRuntime!.InvokeVoid(ScriptLoader, url, options);

    }

    private bool isAbleToUseInProcessRuntime() => _jsInProcessRuntime is not null;
}

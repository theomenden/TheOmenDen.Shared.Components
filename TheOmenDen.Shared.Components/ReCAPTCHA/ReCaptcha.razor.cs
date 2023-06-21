using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using TheOmenDen.Shared.Components.Options;

namespace TheOmenDen.Shared.Components.ReCAPTCHA;

public partial class ReCaptcha : ComponentBase, IDisposable
{
    [Parameter] public string? SiteKey { get; set; }
    [Parameter] public string? ElementId { get; set; } = "recaptcha_container";
    [Parameter] public bool? UseRecaptchaNet { get; set; } = false;
    [Parameter] public bool? UseEnterprise { get; set; } = false;
    [Parameter] public bool? HideBadge { get; set; } = false;

    [Parameter, EditorRequired] public EventCallback<string> OnCallback { get; set; }

    [Parameter, EditorRequired] public EventCallback<string> OnExpired { get; set; }

    [Parameter, EditorRequired] public EventCallback<string> OnError { get; set; }

    [Inject] protected IJSRuntime JSRuntime { get; init; }

    [Inject] protected ILogger<ReCaptcha>? Logger { get; init; }

    private DotNetObjectReference<ReCaptcha>? _dotNetObjectReference;

    private RenderParameters _renderParameters = RenderParameters.Default;

    private ReCaptchaLoaderOptions? _options;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            try
            {
                _renderParameters = _renderParameters with { SiteKey = SiteKey ?? String.Empty };

                _options = new ReCaptchaLoaderOptions(UseRecaptchaNet.GetValueOrDefault(false), UseEnterprise.GetValueOrDefault(false), HideBadge.GetValueOrDefault(false),
                    RenderParameters.Default, null, "");

                _dotNetObjectReference = DotNetObjectReference.Create(this);

                await JSRuntime!.InvokeVoidAsync("window.reCaptchaInterop.loadCaptchaAsync",
                        SiteKey, _options)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error initializing reCAPTCHA");
            }
        }
    }

    [JSInvokable, EditorBrowsable(EditorBrowsableState.Never)]
    public virtual async Task InvokeCallbackAsync(string response)
    {
        await OnCallback.InvokeAsync(response)
            .ConfigureAwait(false);

        Logger.LogInformation("[Captcha Callback]: reCAPTCHA callback invoked");
    }

    [JSInvokable, EditorBrowsable(EditorBrowsableState.Never)]
    public virtual async Task InvokeExpiredAsync(string response)
    {
        await OnExpired.InvokeAsync(response)
            .ConfigureAwait(false);

        Logger.LogInformation("[Captcha Expired]: reCAPTCHA expired");
    }

    [JSInvokable, EditorBrowsable(EditorBrowsableState.Never)]
    public virtual async Task InvokeErrorAsync(string response)
    {
        await OnError.InvokeAsync(response)
            .ConfigureAwait(false);

        Logger.LogError("[Captcha Error]: reCAPTCHA error");
    }

    /// <exception cref="ArgumentNullException"><paramref name="obj" /> is <see langword="null" />.</exception>
    public void Dispose()
    {
        _dotNetObjectReference?.Dispose();
        GC.SuppressFinalize(this);
    }
}


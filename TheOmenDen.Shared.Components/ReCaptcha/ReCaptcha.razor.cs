using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System.ComponentModel;
using TheOmenDen.Shared.Components.ReCaptcha.Settings;

namespace TheOmenDen.Shared.Components.ReCaptcha;
public partial class ReCaptcha : ComponentBase, IAsyncDisposable
{
    #region Injected Services
    [Inject] private IJSRuntime JSRuntime { get; init; }
    [Inject] private ILogger<ReCaptcha> Logger { get; init; }
    #endregion
    #region Parameters
    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public WidgetOptions RenderOptions { get; set; } = WidgetOptions.Default;
    /// <summary>
    /// 
    /// </summary>
    [Parameter, EditorRequired]
    public EventCallback<string> OnCallback { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter, EditorRequired]
    public EventCallback<string> OnScriptLoaded { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Parameter, EditorRequired]
    public EventCallback OnExpired { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Parameter, EditorRequired]
    public EventCallback<string> OnWidgetRendered { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Parameter, EditorRequired]
    public EventCallback<string> OnError { get; set; }

    protected DotNetObjectReference<ReCaptcha>? _dotNetObjectReference { get; set; }
    #endregion
    #region  Lifecycle Methods

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _dotNetObjectReference = DotNetObjectReference.Create(this);
            await JSRuntime.InvokeVoidAsync("loadRecaptcha", RenderOptions)
                .ConfigureAwait(false);
        }
    }
    #endregion
    #region Callback Methods
    [EditorBrowsable(EditorBrowsableState.Never), JSInvokable]
    public async Task OnCallbackAsync(string response)
    {
        if (OnCallback.HasDelegate)
            await OnCallback.InvokeAsync(response)
                .ConfigureAwait(false);
    }

    [EditorBrowsable(EditorBrowsableState.Never), JSInvokable]
    public async Task OnExpiredAsync()
    {
        if (OnExpired.HasDelegate)
            await OnExpired.InvokeAsync(null)
                .ConfigureAwait(false);
    }

    [EditorBrowsable(EditorBrowsableState.Never), JSInvokable]
    public async Task OnErrorAsync(string errorMessage)
    {
        if (OnError.HasDelegate)
            await OnError.InvokeAsync(errorMessage)
                .ConfigureAwait(false);
    }

    [EditorBrowsable(EditorBrowsableState.Never), JSInvokable]
    public async Task OnScriptLoadedAsync(string message)
    {
        if (OnScriptLoaded.HasDelegate)
            await OnScriptLoaded.InvokeAsync(message)
                .ConfigureAwait(false);
    }

    [EditorBrowsable(EditorBrowsableState.Never), JSInvokable]
    public async Task OnWidgetRenderedAsync(string widgetId)
    {
        if (OnWidgetRendered.HasDelegate)
            await OnWidgetRendered.InvokeAsync(widgetId)
                .ConfigureAwait(false);
    }
    #endregion
    #region Disposal Methods
    public async ValueTask DisposeAsync()
    {
        if (_dotNetObjectReference is not null)
        {
            _dotNetObjectReference.Dispose();
            _dotNetObjectReference = null;
        }

        await JSRuntime.InvokeVoidAsync("grecaptcha.reset")
            .ConfigureAwait(false);
    }
    #endregion
}

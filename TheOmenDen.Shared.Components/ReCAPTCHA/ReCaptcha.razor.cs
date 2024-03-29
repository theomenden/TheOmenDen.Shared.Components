﻿using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using TheOmenDen.Shared.Components.Options;
using TheOmenDen.Shared.Guards;

namespace TheOmenDen.Shared.Components.ReCAPTCHA;
/// <summary>
/// A reCAPTCHA component for Blazor - meant for ReCaptcha V3
/// </summary>
/// <remarks>
/// <para>YOU ARE RESPONSIBLE FOR VERIFYING THE TOKEN IN YOUR APPLICATION</para>
/// <para>See <a href="https://developers.google.com/recaptcha/docs/v3">reCAPTCHA V3 docs</a> for more information</para>
/// </remarks>
public partial class ReCaptcha : ComponentBase, IAsyncDisposable
{
    /// <summary>
    /// The site key for the reCAPTCHA
    /// </summary>
    [Parameter, EditorRequired] public string SiteKey { get; set; } = String.Empty;
    /// <summary>
    /// The ID of the element to render the reCAPTCHA in 
    ///  Defaults to "recaptcha_container"
    /// </summary>
    [Parameter] public string? ElementId { get; set; } = "recaptcha_container";
    /// <summary>
    /// If you need to use the recaptcha.net endpoint, set this to true
    /// </summary>
    /// <remarks>See <a href="https://developers.google.com/recaptcha/docs/faq#can-i-use-recaptcha-globally">this part of the docs for more details</a></remarks>
    [Parameter] public bool? UseRecaptchaNet { get; set; } = false;
    /// <summary>
    /// If you are using reCAPTCHA Enterprise, set this to true
    /// </summary>
    [Parameter] public bool? UseEnterprise { get; set; } = false;
    /// <summary>
    /// Allows you to hide the reCAPTCHA badge
    /// </summary>
    /// <remarks>See <a href="https://developers.google.com/recaptcha/docs/faq#id-like-to-hide-the-recaptcha-badge.-what-is-allowed">this part of the docs for more details</a></remarks>
    [Parameter] public bool? HideBadge { get; set; } = false;
    
    /// <summary>
    /// The render parameters for the reCAPTCHA - you can supply the site key here as well
    /// </summary>
    [Parameter] public CaptchaRenderParameters? RenderParameters { get; set; } = null;

    [Parameter, EditorRequired] public ExplicitCaptchaRenderParameters? ExplicitRenderingParameters { get; set; } = null;

    [Parameter, EditorRequired] public EventCallback<string> OnCallback { get; set; }

    /// <summary>
    /// An event that is invoked when the reCAPTCHA token expires
    /// </summary>
    [Parameter, EditorRequired] public EventCallback<string> OnExpired { get; set; }

    /// <summary>
    /// An event that is invoked when an error occurs with the reCAPTCHA
    /// </summary>
    [Parameter, EditorRequired] public EventCallback<string> OnError { get; set; }

    [Inject] protected IJSRuntime? JsRuntime { get; init; }

    [Inject] protected ILogger<ReCaptcha>? Logger { get; init; }

    private DotNetObjectReference<ReCaptcha>? _dotNetObjectReference;
    
    private ReCaptchaLoaderOptions? _options;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            try
            {
                Guard.FromNullOrWhitespace(SiteKey, nameof(SiteKey));

                var isExplicitRender = ExplicitRenderingParameters is not null && RenderParameters is null;

                _options = new ReCaptchaLoaderOptions(SiteKey,UseRecaptchaNet.GetValueOrDefault(false), UseEnterprise.GetValueOrDefault(false), HideBadge.GetValueOrDefault(false), isExplicitRender, String.Empty);

                _dotNetObjectReference = DotNetObjectReference.Create(this);
                
                await ReCaptchaLoaderService.LoadAsync(JsRuntime, _dotNetObjectReference, _options).ConfigureAwait(false);

                await ReCaptchaLoaderService.RenderAsync(JsRuntime, SiteKey, _dotNetObjectReference, RenderParameters).ConfigureAwait(false);
            }
            catch(ArgumentNullException ex)
            {
                Logger?.LogError(ex, "[ReCaptcha]: SiteKey cannot be null");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "[ReCaptcha]: Error initializing reCAPTCHA");
            }
        }
    }


    /// <summary>
    /// Invoked when the captcha has been executed
    /// </summary>
    /// <param name="response"></param>
    /// <returns><see cref="Task"/></returns>
    [JSInvokable, EditorBrowsable(EditorBrowsableState.Never)]
    public virtual async Task InvokeCallbackAsync(string response)
    {
        await OnCallback.InvokeAsync(response)
            .ConfigureAwait(false);

        Logger?.LogInformation("[Captcha Callback]: reCAPTCHA callback invoked");
    }

    /// <summary>
    /// Invoked when the captcha token has expired and needs to be retried
    /// </summary>
    /// <param name="response"></param>
    /// <returns><see cref="Task"/></returns>
    [JSInvokable, EditorBrowsable(EditorBrowsableState.Never)]
    public virtual async Task InvokeExpiredAsync(string response)
    {
        await OnExpired.InvokeAsync(response)
            .ConfigureAwait(false);

        Logger?.LogInformation("[Captcha Expired]: reCAPTCHA expired");
    }

    /// <summary>
    /// Provides a way to handle errors from the reCAPTCHA
    /// </summary>
    /// <param name="response">The response from invoking the captcha</param>
    /// <returns><see cref="Task"/></returns>
    [JSInvokable, EditorBrowsable(EditorBrowsableState.Never)]
    public virtual async Task InvokeErrorAsync(string response)
    {
        await OnError.InvokeAsync(response)
            .ConfigureAwait(false);

        Logger?.LogError("[Captcha Error]: reCAPTCHA error");
    }

    [JSInvokable, EditorBrowsable(EditorBrowsableState.Never)]
    public virtual Task InvokeCaptchaRenderedAsync(int widgetId)
    {
        Logger?.LogInformation($"[Captcha Rendered]: reCAPTCHA rendered with widget ID {widgetId}");
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if (_dotNetObjectReference is not null)
        {
            await JsRuntime.InvokeVoidAsync("window.reCaptchaInterop.resetAsync")
                               .ConfigureAwait(false);
            _dotNetObjectReference?.Dispose();
            _dotNetObjectReference = null;
        }
        GC.SuppressFinalize(this);
    }
}


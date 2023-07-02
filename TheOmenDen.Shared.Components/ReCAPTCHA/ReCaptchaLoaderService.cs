using Microsoft.JSInterop;
using TheOmenDen.Shared.Components.Constants;
using TheOmenDen.Shared.Components.Options;
using TheOmenDen.Shared.Guards;

namespace TheOmenDen.Shared.Components.ReCAPTCHA;
internal static class ReCaptchaLoaderService
{
    public static async Task LoadAsync(IJSRuntime jsRuntime, DotNetObjectReference<ReCaptcha> dotNetObjectReference,
        ReCaptchaLoaderOptions? options)
    {
        Guard.FromNull(options, nameof(options));

        var url = options!.ShouldUseRecaptchaNet
            ? CaptchaConstants.ReCaptchaNetBase
            : CaptchaConstants.GoogleCaptchaBase;

        url += options!.ShouldRenderExplicitly 
            ? GetExplicitCaptchaUrl(options.SiteKey) 
            : GetStandardCaptchaUrl(options.SiteKey);
        
        await jsRuntime.InvokeVoidAsync("window.reCaptchaInterop.loadAsync", url, dotNetObjectReference,
            new
            {
                id = "loadedCaptchaScript",
                isAsync = true,
                isDeferred = true,
                appendedTo = "head",
                maxRetries = 3,
                retryInterval = 25
            });
    }

    public static async Task RenderAsync(IJSRuntime jsRuntime, string siteKey, DotNetObjectReference<ReCaptcha> dotNetObjectReference,
        CaptchaRenderParameters? renderParameters)
    {
        Guard.FromNullOrWhitespace(siteKey, nameof(siteKey));

        await jsRuntime.InvokeVoidAsync("window.reCaptchaInterop.renderAsync", siteKey, dotNetObjectReference, renderParameters);
    }

    public static async Task RenderAsync(IJSRuntime jsRuntime, string siteKey, DotNetObjectReference<ReCaptcha> dotNetObjectReference,
        ExplicitCaptchaRenderParameters? renderParameters)
    {
        Guard.FromNullOrWhitespace(siteKey, nameof(siteKey));

        await jsRuntime.InvokeVoidAsync("window.reCaptchaInterop.renderAsync", siteKey, dotNetObjectReference, renderParameters);
    }

    private static string GetStandardCaptchaUrl(string siteKey)
    => $"?render={siteKey}";

    private static string GetExplicitCaptchaUrl(string siteKey)
    => $"?render=explicit&sitekey={siteKey}";
}

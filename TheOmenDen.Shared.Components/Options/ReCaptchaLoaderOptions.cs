namespace TheOmenDen.Shared.Components.Options;

internal sealed record ReCaptchaLoaderOptions(
    bool ShouldUseRecaptchaNet,
    bool ShouldUseEnterprise,
    bool ShouldHideBadgeAutomatically,
    CaptchaRenderParameters? RenderParameters,
    ExplicitCaptchaRenderParameters? ExplicitCaptchaRenderParameters,
    string CustomResourceUrl
);

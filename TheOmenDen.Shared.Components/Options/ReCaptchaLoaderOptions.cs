namespace TheOmenDen.Shared.Components.Options;

public sealed record ReCaptchaLoaderOptions(
    bool ShouldUseRecaptchaNet,
    bool ShouldUseEnterprise,
    bool ShouldHideBadgeAutomatically,
    RenderParameters? RenderParameters,
    ExplicitCaptchaRenderParameters? ExplicitCaptchaRenderParameters,
    string CustomResourceUrl
);

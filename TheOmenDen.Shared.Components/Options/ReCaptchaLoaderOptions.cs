namespace TheOmenDen.Shared.Components.Options;

internal sealed record ReCaptchaLoaderOptions(
    string SiteKey,
    bool ShouldUseRecaptchaNet,
    bool ShouldUseEnterprise,
    bool ShouldHideBadgeAutomatically,
    bool ShouldRenderExplicitly,
    string CustomResourceUrl
);

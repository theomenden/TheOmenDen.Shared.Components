using TheOmenDen.Shared.Components.Enumerations;

namespace TheOmenDen.Shared.Components.Options;

public sealed record ExplicitCaptchaRenderParameters(
    string Container,
    CaptchaBadgeLocations BadgeLocation,
    CaptchaSizes Size,
    CaptchaThemes Theme,
    int TabIndex);

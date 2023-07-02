using TheOmenDen.Shared.Components.Enumerations;

namespace TheOmenDen.Shared.Components.Options;
public sealed record CaptchaRenderParameters(string SiteKey,
                                 CaptchaBadgeLocations BadgeLocation,
                                 CaptchaSizes Size,
                                 CaptchaThemes Theme,
                                 int TabIndex)
{
    public static readonly CaptchaRenderParameters Default = new(
        SiteKey: String.Empty,
        BadgeLocation: CaptchaBadgeLocations.BottomRight,
        Size: CaptchaSizes.Compact,
        Theme: CaptchaThemes.Light,
        TabIndex: 0);
}

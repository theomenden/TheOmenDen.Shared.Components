using TheOmenDen.Shared.Components.Enumerations;

namespace TheOmenDen.Shared.Components.Options;
public sealed record CaptchaRenderParameters(string SiteKey,
                                 CaptchaBadgeLocations BadgeLocation,
                                 CaptchaSizes Size,
                                 CaptchaThemes Theme,
                                 int TabIndex)
{
    public static readonly CaptchaRenderParameters Default = new(
        SiteKey: string.Empty,
        BadgeLocation: CaptchaBadgeLocations.BottomRight,
        Size: CaptchaSizes.Invisible,
        Theme: CaptchaThemes.Light,
        TabIndex: 0);
}

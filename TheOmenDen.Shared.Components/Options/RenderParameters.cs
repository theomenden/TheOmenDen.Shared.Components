using Microsoft.AspNetCore.Authorization;
using TheOmenDen.Shared.Components.Enumerations;

namespace TheOmenDen.Shared.Components.Options;

public sealed record RenderParameters(string SiteKey,
                                        CaptchaBadgeLocations BadgeLocation,
                                        CaptchaSizes Size,
                                        int TabIndex)
{
    public static readonly RenderParameters Default = new(
               SiteKey: string.Empty,
                      BadgeLocation: CaptchaBadgeLocations.BottomRight,
                      Size: CaptchaSizes.Invisible,
                      TabIndex: 0);
}

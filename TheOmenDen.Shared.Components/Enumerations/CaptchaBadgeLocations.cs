using TheOmenDen.Shared.Enumerations;

namespace TheOmenDen.Shared.Components.Enumerations;
public sealed record CaptchaBadgeLocations : EnumerationBase<CaptchaBadgeLocations>
{
    private CaptchaBadgeLocations(string name, int id) : base(name, id) { }

    public static readonly CaptchaBadgeLocations BottomRight = new(nameof(BottomRight).ToLowerInvariant(), 0);
    public static readonly CaptchaBadgeLocations BottomLeft = new(nameof(BottomLeft).ToLowerInvariant(), 1);
    public static readonly CaptchaBadgeLocations Inline = new(nameof(Inline).ToLowerInvariant(), 2);
}

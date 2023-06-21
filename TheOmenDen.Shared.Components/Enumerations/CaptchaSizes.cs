using TheOmenDen.Shared.Enumerations;

namespace TheOmenDen.Shared.Components.Enumerations;

/// <summary>
/// The size of the widget
/// </summary>
public sealed record CaptchaSizes : EnumerationBase<CaptchaSizes>
{
    private CaptchaSizes(string name, int id) : base(name, id)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public static readonly CaptchaSizes Normal = new(nameof(Normal).ToLowerInvariant(), 0);
    /// <summary>
    /// 
    /// </summary>
    public static readonly CaptchaSizes Compact = new(nameof(Compact).ToLowerInvariant(), 1);
    /// <summary>
    /// 
    /// </summary>
    public static readonly CaptchaSizes Invisible = new(nameof(Invisible).ToLowerInvariant(), 2);
}

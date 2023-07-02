using TheOmenDen.Shared.Enumerations;

namespace TheOmenDen.Shared.Components.Enumerations;
public sealed record CaptchaThemes : EnumerationBase<CaptchaThemes>
{
    private CaptchaThemes(string name, int id) : base(name, id) { }

    public static readonly CaptchaThemes Light = new("light", 0);
    public static readonly CaptchaThemes Dark = new("dark", 1);
}

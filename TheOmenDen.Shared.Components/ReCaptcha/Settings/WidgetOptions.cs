using TheOmenDen.Shared.Components.ReCaptcha.Enumerations;

namespace TheOmenDen.Shared.Components.ReCaptcha.Settings;

public sealed record WidgetOptions(
    string SiteKey,
    WidgetTheme Theme,
    WidgetSize Size,
    WidgetLocation Location,
    int TabIndex)
{
    public static readonly WidgetOptions Default = new(
        string.Empty,
        WidgetTheme.Light,
        WidgetSize.Normal,
        WidgetLocation.BottomRight,
        0);
}
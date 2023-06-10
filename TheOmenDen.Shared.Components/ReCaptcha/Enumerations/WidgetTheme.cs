using TheOmenDen.Shared.Enumerations;

namespace TheOmenDen.Shared.Components.ReCaptcha.Enumerations;
/// <summary>
/// Determines the theme of the widget.
/// </summary>
public sealed record WidgetTheme : EnumerationBase<WidgetTheme>
{
    private WidgetTheme(string name, int id) : base(name, id)
    {
    }

    /// <summary>
    /// Renders the widget in a Dark theme.
    /// </summary>
    public static readonly WidgetTheme Dark = new("dark", 1);
    /// <summary>
    /// Renders the widget in a Light theme.
    /// </summary>
    public static readonly WidgetTheme Light = new("light", 2);
}

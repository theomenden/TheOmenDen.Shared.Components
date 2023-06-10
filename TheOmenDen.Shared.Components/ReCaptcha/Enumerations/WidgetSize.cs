using TheOmenDen.Shared.Enumerations;

namespace TheOmenDen.Shared.Components.ReCaptcha.Enumerations;
/// <summary>
/// Determines the size of the widget.
/// </summary>
public sealed record WidgetSize : EnumerationBase<WidgetSize>
{
    private WidgetSize(string name, int id) : base(name, id)
    {
    }

    /// <summary>
    /// Renders the widget in a more compact form.
    /// </summary>
    public static readonly WidgetSize Compact = new("compact", 1);
    /// <summary>
    /// Renders the widget in the normal size.
    /// </summary>
    public static readonly WidgetSize Normal = new("normal", 2);
}
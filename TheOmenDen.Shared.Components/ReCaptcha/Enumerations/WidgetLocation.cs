using TheOmenDen.Shared.Enumerations;

namespace TheOmenDen.Shared.Components.ReCaptcha.Enumerations;

/// <summary>
/// Determines the location of the widget
/// </summary>
public sealed record WidgetLocation : EnumerationBase<WidgetLocation>
{
    private WidgetLocation(string name, int id) : base(name, id)
    {
    }

    /// <summary>
    /// Loads the widget on the bottom right of the page.
    /// </summary>
    public static readonly WidgetLocation BottomRight = new("bottomright", 1);
    /// <summary>
    /// Loads the widget on the bottom left of the page.
    /// </summary>
    public static readonly WidgetLocation BottomLeft = new("bottomleft", 2);
    /// <summary>
    /// Loads the widget inline.
    /// </summary>
    public static readonly WidgetLocation Inline = new("inline", 3);
}

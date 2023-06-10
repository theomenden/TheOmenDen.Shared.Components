using TheOmenDen.Shared.Enumerations;

namespace TheOmenDen.Shared.Components.ReCaptcha.Enumerations;
/// <summary>
/// Controls the rendering mode for the widget.
/// </summary>
public sealed record WidgetRenderMode : EnumerationBase<WidgetRenderMode>
{
    private WidgetRenderMode(string name, int id) : base(name, id)
    {
    }

    /// <summary>
    /// Loads the widget explicitly.
    /// </summary>
    public static readonly WidgetRenderMode Explicit = new("explicit", 1);
    /// <summary>
    /// Default - loads the widget in the first g-recaptcha tag it finds
    /// </summary>
    public static readonly WidgetRenderMode Onload = new("onload", 2);
}

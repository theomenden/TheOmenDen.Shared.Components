namespace TheOmenDen.Shared.Components.AudioDisplay.Models.Options;

public sealed record HowlerConfiguration
{
    public HowlerConfiguration()
    {}

    public HowlerConfiguration(IEnumerable<String> sources)
    {
        Sources = sources;
    }

    public HowlerConfiguration(IEnumerable<String> sources, IEnumerable<String> codecs)
    {
        Sources = sources;
        Codecs = codecs;
    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<String> Sources { get; } = Enumerable.Empty<String>();

    /// <summary>
    /// A collection of additional codecs in the case that Howler cannot detect them, or you need to supply ones that are normally not supported
    /// </summary>
    public IEnumerable<String> Codecs { get; } = Enumerable.Empty<String>();

    /// <summary>
    /// Determines if we should use the built in HTML5 Audio Player
    /// </summary>
    public Boolean IsHtml5Audio { get; } = false;

    /// <summary>
    /// 
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the audio should loop <see langword="false"/> otherwise
    /// </value>
    public Boolean IsLooping { get; } = false;

    /// <summary>
    /// Determines the volume level
    /// </summary>
    /// <value>0.00 to 1.00</value>
    public Double Volume { get; } = 1d;
}

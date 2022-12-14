
namespace TheOmenDen.Shared.Components.AudioDisplay;

public interface IAudioPlayer
{
    /// <summary>
    /// Mutes or unmutes all sounds
    /// </summary>
    /// <param name="isMuted"></param>
    /// <returns></returns>
    public ValueTask Mute(Boolean isMuted);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public ValueTask<IEnumerable<String>> GetCodecs();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="codecExtension"></param>
    /// <returns></returns>
    public ValueTask<Boolean> IsCodecSupported(String? codecExtension);
}


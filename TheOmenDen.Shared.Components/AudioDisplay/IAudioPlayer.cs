
namespace TheOmenDen.Shared.Components.AudioDisplay;

public interface IAudioPlayer
{
    public ValueTask Mute(Boolean IsMuted);

    public ValueTask<IEnumerable<Stream>> GetCodecs();

    public ValueTask<Boolean> IsCodeSupported(String? codecExtension);
}


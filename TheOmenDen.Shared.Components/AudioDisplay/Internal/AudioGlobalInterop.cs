using Microsoft.JSInterop;

namespace TheOmenDen.Shared.Components.AudioDisplay.Internal;

public sealed class AudioGlobalInterop : IAudioPlayer
{
    private const double MaximumRate = 4d;
    private const double MinimumRate = 0.25;

    private readonly IJSRuntime _runtime;

    public AudioGlobalInterop(IJSRuntime runtime)
    {
        _runtime = runtime ?? throw new ArgumentNullException(nameof(runtime));
    }

    public ValueTask<IEnumerable<String>> GetCodecs()
    {
        return _runtime.InvokeAsync<IEnumerable<String>>("howler.getCodecs");
    }

    public ValueTask<bool> IsCodecSupported(string? codecExtension)
    {
        return _runtime.InvokeAsync<Boolean>("howler.isCodecSupported", codecExtension);
    }

    public ValueTask Mute(bool IsMuted)
    {
        return _runtime.InvokeVoidAsync("howler.mute", IsMuted);
    }
}
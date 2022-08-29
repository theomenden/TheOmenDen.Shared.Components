using Microsoft.JSInterop;
using TheOmenDen.Shared.Extensions;

namespace TheOmenDen.Shared.Components.AudioDisplay.Internal;

public sealed class AudioGlobalInterop : IAudioPlayer
{
    private const double MaximumRate = 4d;
    private const double MinimumRate = 0.25;

    private readonly AsyncLazyInitializer<IJSObjectReference> _runtimeReference;

    private readonly IJSRuntime _runtime;

    public AudioGlobalInterop(IJSRuntime runtime)
    {
        _runtime = runtime ?? throw new ArgumentNullException(nameof(runtime));

        _runtimeReference = new(() => _runtime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/TheOmenDen.Shared.Components/wwwroot/audiojsinterop.min.js").AsTask());
    }

    public async ValueTask<IEnumerable<String>> GetCodecs()
    {
        var runtime = await _runtimeReference;
        
        return await runtime.InvokeAsync<IEnumerable<String>>("howler.getCodecs");
    }

    public async ValueTask<bool> IsCodecSupported(string? codecExtension)
    {
        var runtime = await _runtimeReference;

        return await runtime.InvokeAsync<Boolean>("howler.isCodecSupported", codecExtension);
    }

    public async ValueTask Mute(bool isMuted)
    {
        var runtime = await _runtimeReference;

        await runtime.InvokeVoidAsync("howler.mute", isMuted);
    }
}
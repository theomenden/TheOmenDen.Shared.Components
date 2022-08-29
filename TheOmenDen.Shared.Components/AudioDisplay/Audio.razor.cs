using Microsoft.AspNetCore.Components;
using TheOmenDen.Shared.Components.AudioDisplay.Internal;
using TheOmenDen.Shared.Components.AudioDisplay.Models;

namespace TheOmenDen.Shared.Components.AudioDisplay;

public partial class Audio : ComponentBase
{
    [Inject] AudioController AudioController { get; init; }

    [Inject] AudioGlobalInterop AudioGlobalInterop { get; init; }

    [Parameter] public Boolean Muted { get; set; } = true;
    
    [Parameter] public Boolean Autoplay { get; set; } = true;

    [Parameter] public AudioSource? Source { get; set; }

    [Parameter] public String CodeNotSupportedMessage { get; set; } = "Your browser does not support the audio element.";

    private String _supportedCodecs = String.Empty;

    private String _errorMessage = String.Empty;

    private Double _rate = 0d;

    private TimeSpan _duration = TimeSpan.Zero;

    private String _state = String.Empty;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        RegisterAudioControllerEvents();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            var codecs = await AudioGlobalInterop.GetCodecs();

            _supportedCodecs = String.Join(',', codecs);

            StateHasChanged();
        }
    }

    private void RegisterAudioControllerEvents()
    {
        AudioController.OnPlay += e =>
        {
            _errorMessage = String.Empty;
            _state = AudioStates.Playing.Name;
            _duration = e.Duration;

            StateHasChanged();
        };

        AudioController.OnStop += e =>
        {
            _state = AudioStates.Stopped.Name;

            StateHasChanged();
        };

        AudioController.OnPause += e =>
        {
            _state = AudioStates.Paused.Name;

            StateHasChanged();
        };

        AudioController.OnRate += e => { _rate = e.CurrentRate; };

        AudioController.OnPlayError += e =>
        {
            _errorMessage = $"OnPlayError: {e.Message}";

            StateHasChanged();
        };

        AudioController.OnLoadError += e =>
        {
            _errorMessage = $"OnLoadError: {e.Message}";

            StateHasChanged();
        };
    }

    private async Task PauseAsync()
    {
        foreach (var id in Source.Song.MusicUris)
        {
            await AudioController.PauseAsync(id);
        }
    }
}
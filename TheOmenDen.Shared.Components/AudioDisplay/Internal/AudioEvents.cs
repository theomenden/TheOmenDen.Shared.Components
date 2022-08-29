using TheOmenDen.Shared.Components.AudioDisplay.Models.Events;

namespace TheOmenDen.Shared.Components.AudioDisplay.Internal;

public partial class AudioController
{
    public event Action<AudioPlayEventArgs>? OnPlay;
    public event Action<AudioErrorEventArgs>? OnPlayError;
    public event Action<EventArgs>? OnLoad;
    public event Action<AudioErrorEventArgs>? OnLoadError;
    public event Action<AudioEventArgs>? OnStop;
    public event Action<AudioEventArgs>? OnPause;
    public event Action<AudioErrorEventArgs>? OnStopError;
    public event Action<AudioEventArgs>? OnEnd;
    public event Action<AudioEventArgs>? OnVolume;
    public event Action<AudioEventArgs>? OnMute;
    public event Action<AudioEventArgs>? OnFade;
    public event Action<EventArgs>? OnUnlock;
    public event Action<AudioRateEventArgs>? OnRate;
    public event Action<AudioEventArgs>? OnSeek;
}
using Microsoft.JSInterop;
using TheOmenDen.Shared.Components.AudioDisplay.Models.Events;

namespace TheOmenDen.Shared.Components.AudioDisplay.Internal;

public partial class AudioController
{
    [JSInvokable]
    public void OnPlayCallback(Int32 soundId, Double? durationInSeconds)
    {
        if (durationInSeconds is not null)
        {
            TotalTime = TimeSpan.FromSeconds(durationInSeconds.Value);
        }

        OnPlay?.Invoke(new AudioPlayEventArgs { SoundId = soundId, Duration = TotalTime });
    }

    [JSInvokable]
    public void OnStopCallback(Int32 soundId)
    {
        TotalTime = TimeSpan.Zero;

        OnStop?.Invoke(new AudioEventArgs { SoundId = soundId });
    }

    [JSInvokable]
    public void OnPauseCallback(Int32 soundId)
    {
        TotalTime = TimeSpan.Zero;

        OnPause?.Invoke(new AudioEventArgs { SoundId = soundId });
    }

    [JSInvokable]
    public void OnRateCallback(Int32 soundId, Double currentRate)
    {
        OnRate?.Invoke(new AudioRateEventArgs { SoundId = soundId, CurrentRate = currentRate });
    }

    public void OnEndCallback(Int32 soundId)
    {
        OnEnd?.Invoke(new AudioEventArgs { SoundId = soundId });
    }

    [JSInvokable]
    public void OnLoadCallback()
    {
        OnLoad?.Invoke(EventArgs.Empty);
    }

    [JSInvokable]
    public void OnLoadErrorCallback(Int32? soundId, String error)
    {
        OnLoadError?.Invoke(new AudioErrorEventArgs { SoundId = soundId, Message = error });
    }

    [JSInvokable]
    public void OnPlayErrorCallback(Int32? soundId, String error)
    {
        OnPlayError?.Invoke(new AudioErrorEventArgs { SoundId = soundId, Message = error });
    }
}

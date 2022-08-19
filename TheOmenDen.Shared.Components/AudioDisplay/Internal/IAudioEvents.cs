using TheOmenDen.Shared.Components.AudioDisplay.Models.Events;

namespace TheOmenDen.Shared.Components.AudioDisplay.Internal;
public interface IAudioEvents
{
    #region OnPlay Events
    /// <summary>
    /// 
    /// </summary>
    event Action<AudioPlayEventArgs>? OnPlay;
    /// <summary>
    /// 
    /// </summary>
    event Action<AudioErrorEventArgs>? OnPlayError;
    #endregion
    #region Loading Events
    /// <summary>
    /// 
    /// </summary>
    event Action<EventArgs>? OnLoad;
    /// <summary>
    /// 
    /// </summary>
    event Action<AudioErrorEventArgs>? OnLoadError;
    #endregion
    #region OnStop Events
    /// <summary>
    /// 
    /// </summary>
    event Action<AudioEventArgs>? OnStop;
    /// <summary>
    /// 
    /// </summary>
    event Action<AudioErrorEventArgs>? OnStopError;
    /// <summary>
    /// 
    /// </summary>
    event Action<AudioEventArgs>? OnEnd;
    #endregion
    #region Volume Control Events
    /// <summary>
    /// 
    /// </summary>
    event Action<AudioEventArgs>? OnVolume;
    /// <summary>
    /// 
    /// </summary>
    event Action<AudioEventArgs>? OnMute;
    /// <summary>
    /// 
    /// </summary>
    event Action<AudioEventArgs>? OnFade;
    #endregion
    #region OnLocking Events
    /// <summary>
    /// 
    /// </summary>
    event Action<EventArgs>? OnUnlock;
    #endregion
    #region Timing Events
    /// <summary>
    /// 
    /// </summary>
    event Action<AudioRateEventArgs>? OnRate;
    /// <summary>
    /// 
    /// </summary>
    event Action<AudioEventArgs>? OnSeek;
    #endregion
}


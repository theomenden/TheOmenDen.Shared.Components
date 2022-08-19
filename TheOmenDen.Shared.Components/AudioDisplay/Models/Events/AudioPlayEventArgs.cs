namespace TheOmenDen.Shared.Components.AudioDisplay.Models.Events;

public class AudioPlayEventArgs : AudioEventArgs
{
    /// <summary>
    /// 
    /// </summary>
    public TimeSpan Duration { get; set; }
}
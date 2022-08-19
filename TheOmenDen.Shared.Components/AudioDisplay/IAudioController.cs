using TheOmenDen.Shared.Components.AudioDisplay.Internal;
using TheOmenDen.Shared.Components.AudioDisplay.Models.Options;

namespace TheOmenDen.Shared.Components.AudioDisplay;
public interface IAudioController: IAudioEvents
{
    #region Properties
    /// <summary>
    /// 
    /// </summary>
    TimeSpan TotalTime { get; }
    #endregion
    #region Play Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    ValueTask<Int32> PlayAsync(Uri resource);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resources"></param>
    /// <returns></returns>
    ValueTask<Int32> PlayAsync(IEnumerable<Uri> resources);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resources"></param>
    /// <returns></returns>
    ValueTask<Int32> PlayAsync(params Uri[] resources);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    ValueTask<Int32> PlayAsync(string resource);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resources"></param>
    /// <returns></returns>
    ValueTask<Int32> PlayAsync(IEnumerable<String> resources);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resources"></param>
    /// <returns></returns>
    ValueTask<Int32> PlayAsync(params string[] resources);

    ValueTask<Int32> PlayAsync(HowlerConfiguration configuration);

    ValueTask<Int32> PlayAsync(Byte[] audio, String mimeType);
    #endregion
    #region Rate Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="soundId"></param>
    /// <param name="rate"></param>
    /// <returns></returns>
    ValueTask RateAsync(Int32 soundId, Double rate);
    #endregion
    #region Seek Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="soundId"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    ValueTask SeekAsync(Int32 soundId, TimeSpan position);
    #endregion
    #region Stopping Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="soundId"></param>
    /// <returns></returns>
    ValueTask StopAsync(Int32 soundId);
    #endregion
    #region Pausing Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="soundId"></param>
    /// <returns></returns>
    ValueTask PauseAsync(Int32 soundId);
    #endregion
    #region Loading Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="soundId"></param>
    /// <returns></returns>
    ValueTask LoadAsync(Int32 soundId);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="soundId"></param>
    /// <returns></returns>
    ValueTask UnloadAsync(Int32 soundId);
    #endregion
    #region Time Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="soundId"></param>
    /// <returns></returns>
    ValueTask<TimeSpan> GetCurrentTimeAsync(Int32 soundId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="soundId"></param>
    /// <returns></returns>
    ValueTask<TimeSpan> GetTotalTimeAsync(Int32 soundId);
    #endregion
    #region Boolean Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="soundId"></param>
    /// <returns></returns>
    ValueTask<Boolean> IsCurrentlyPlayingAsync(Int32 soundId);
    #endregion
}


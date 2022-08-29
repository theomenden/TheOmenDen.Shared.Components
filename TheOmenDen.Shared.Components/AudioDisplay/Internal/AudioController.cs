﻿using Microsoft.JSInterop;
using TheOmenDen.Shared.Components.AudioDisplay.Models.Options;
using TheOmenDen.Shared.Extensions;

namespace TheOmenDen.Shared.Components.AudioDisplay.Internal;
public partial class AudioController : IAudioController, IDisposable, IAsyncDisposable
{
    #region Private fields
    private readonly IJSRuntime _runtime;

    private readonly AsyncLazyInitializer<IJSObjectReference> _runtimeReference;

    private readonly DotNetObjectReference<AudioController> _dotNetObjectReference;

    private bool _isDisposed;
    #endregion
    public TimeSpan TotalTime { get; private set; }
    #region Constructors
    public AudioController(IJSRuntime runtime)
    {
        _runtime = runtime ?? throw new ArgumentNullException(nameof(runtime));

        _runtimeReference = new(() => _runtime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/TheOmenDen.Shared.Components/wwwroot/audiojsinterop.min.js").AsTask());

        _dotNetObjectReference = DotNetObjectReference.Create(this);
    }
    #endregion
    #region Play Methods
    public ValueTask PlayAsync(Int32 soundId)
    {
        return _runtime.InvokeVoidAsync("howl.playSound", soundId);
    }

    public ValueTask<int> PlayAsync(Uri resource)
    {
        if (resource is null)
        {
            throw new ArgumentNullException(nameof(resource));
        }

        var options = new HowlerConfiguration(new[] { resource.ToString() });

        return PlayAsync(options);
    }

    public ValueTask<int> PlayAsync(IEnumerable<Uri> resources)
    {
        var resourceList = resources?.ToList() ?? new List<Uri>(1);

        if (resourceList.Any())
        {
            throw new ArgumentNullException(nameof(resources));
        }

        var options = new HowlerConfiguration(resourceList.Select(l => l.ToString()));

        return PlayAsync(options);
    }

    public ValueTask<int> PlayAsync(params Uri[] resources)
    {
        if (resources is null || !resources.Any())
        {
            throw new ArgumentNullException(nameof(resources));
        }

        var options = new HowlerConfiguration(resources.Select(l => l.ToString()));

        return PlayAsync(options);
    }

    public ValueTask<int> PlayAsync(string resource)
    {
        if (String.IsNullOrWhiteSpace(resource))
        {
            throw new ArgumentNullException(nameof(resource));
        }

        var options = new HowlerConfiguration(new[] { resource });

        return PlayAsync(options);
    }

    public ValueTask<int> PlayAsync(IEnumerable<string> resources)
    {
        var resourceList = resources?.ToList() ?? new List<string>(1);
        if (resourceList.Any())
        {
            throw new ArgumentNullException(nameof(resources));
        }

        var options = new HowlerConfiguration(resourceList);

        return PlayAsync(options);
    }

    public ValueTask<int> PlayAsync(params string[] resources)
    {
        if (resources is null || !resources.Any())
        {
            throw new ArgumentNullException(nameof(resources));
        }

        var options = new HowlerConfiguration(resources);

        return PlayAsync(options);
    }

    public ValueTask<int> PlayAsync(byte[] audio, string mimetype)
    {
        if (audio is null || !audio.Any())
        {
            throw new ArgumentNullException(nameof(audio));
        }

        if (String.IsNullOrWhiteSpace(mimetype))
        {
            throw new ArgumentNullException(nameof(mimetype));
        }

        var audioBase64 = Convert.ToBase64String(audio);

        var html5AudioUrl = $"data:{mimetype};base64,{audioBase64}";

        var options = new HowlerConfiguration(new[] { html5AudioUrl });

        return _runtime.InvokeAsync<int>("howl.play", _dotNetObjectReference, options);
    }

    public ValueTask<int> PlayAsync(HowlerConfiguration configuration)
    {
        if (configuration?.Sources is not null
           && !configuration.Sources.Any())
        {
            throw new ArgumentNullException(nameof(configuration.Sources));
        }

        return _runtime.InvokeAsync<int>("howl.play", _dotNetObjectReference, configuration);
    }
    #endregion
    #region Audio Manipulation Methods
    public async ValueTask RateAsync(int soundId, double rate)
    {
        var runtime = await _runtimeReference.Value;

        await runtime.InvokeVoidAsync("howl.rate", soundId, rate);
    }

    public async ValueTask SeekAsync(int soundId, TimeSpan position)
    {
        var runtime = await _runtimeReference.Value;

        await runtime.InvokeVoidAsync("howl.seek", soundId, position.TotalSeconds);
    }

    public async ValueTask StopAsync(int soundId)
    {
        var runtime = await _runtimeReference.Value;

        await runtime.InvokeVoidAsync("howl.stop", soundId);
    }

    public async ValueTask PauseAsync(int soundId)
    {
        var runtime = await _runtimeReference.Value;

        await runtime.InvokeVoidAsync("howl.pause", soundId);
    }

    public async ValueTask LoadAsync(int soundId)
    {
        var runtime = await _runtimeReference.Value;

        await runtime.InvokeVoidAsync("howl.load", soundId);
    }

    public async ValueTask UnloadAsync(int soundId)
    {
        var runtime = await _runtimeReference.Value;

        await runtime.InvokeVoidAsync("howl.unload", soundId);
    }
    #endregion
    #region Timing Methods
    public async ValueTask<TimeSpan> GetCurrentTimeAsync(int soundId)
    {
        var runtime = await _runtimeReference.Value;

        var timeInSeconds = await runtime.InvokeAsync<double?>("howl.getCurrentTime", soundId);

        return ConvertToTimespan(timeInSeconds);
    }

    public async ValueTask<TimeSpan> GetTotalTimeAsync(int soundId)
    {
        var runtime = await _runtimeReference.Value;


        var timeInSeconds = await runtime.InvokeAsync<double?>("howl.getTotalTime", soundId);

        return ConvertToTimespan(timeInSeconds);
    }
    #endregion
    public async ValueTask<bool> IsCurrentlyPlayingAsync(int soundId)
    {
        var runtime = await _runtimeReference.Value;

        return await runtime.InvokeAsync<bool>("howl.getIsPlaying", soundId);
    }
    #region Private Methods
    private static TimeSpan ConvertToTimespan(Double? value)
    {
        return value is null ? TimeSpan.Zero : TimeSpan.FromSeconds(value.Value);
    }
    #endregion
    #region Reference Destruction
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed)
        {
            return;
        }

        if (disposing)
        {
            _runtime.InvokeVoidAsync("howl.destroy");

            _dotNetObjectReference.Dispose();
        }

        _isDisposed = true;
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public ValueTask DisposeAsync()
    {
        if (!_isDisposed)
        {
            _runtime.InvokeVoidAsync("howl.destroy");

            _dotNetObjectReference.Dispose();

            _isDisposed = true;
        }

        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
    #endregion
}

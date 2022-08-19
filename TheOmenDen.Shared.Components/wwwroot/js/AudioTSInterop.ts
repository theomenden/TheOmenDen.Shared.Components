import { Howl, Howler } from 'howler';
import { AudioWindow } from 'audiowindow';

declare let window: AudioWindow;

const audioInstances = {};

window.audio = {
    play: function (dotnetReference, options) {

        const sound = new Howl({
            src: options.sources,
            format: options.formats,
            html5: options.html5,
            loop: options.loop,
            volume: options.volume,

            onplay: async function (id) {
                let duration = audio.duration(id);

                if (duration === Infinity || isNaN(duration)) {
                    duration = null;
                }

                await dotnetReference.invokeMethodAsync("OnPlayCallback", id, duration);
            },
            onstop: async function (id) {
                await dotnetReference.invokeMethodAsync("OnStopCallback", id);
            },
            onpause: async function (id) {
                await dotnetReference.invokeMethodAsync("OnPauseCallback", id);
            },
            onrate: async function (id) {
                const currentRate = audio.rate();
                await dotnetReference.invokeMethodAsync("OnRateCallback", id, currentRate);
            },
            onend: async function (id) {
                await dotnetReference.invokeMethodAsync("OnEndCallback", id);
            },
            onload: async function () {
                await dotnetReference.invokeMethodAsync("OnLoadCallback");
            },
            onloaderror: async function (id, error) {
                await dotnetReference.invokeMethodAsync("OnLoadErrorCallback", id, error);
            },
            onplayerror: async function (id, error) {
                await dotnetReference.invokeMethodAsync("OnPlayErrorCallback", id, error);
            }
        });

        let soundId = sound.play();

        audioInstances[soundId] = {
            sound,
            options
        };

        return soundId;
    },
    playSound: function (id) {
        const audio = getAudio(id);
        if (audio) {
            audio.play(id);
        }
    },
    stop: function (id) {
        const audio = getAudio(id);
        if (audio) {
            audio.stop();
        }
    },
    pause: function (id) {
        const audio = getAudio(id);
        if (audio?.playing()) {
            audio.pause(id);
            return;
        }

        audio.play(id);
    },
    seek: function (id, postiion) {
        const audio = getAudio(id);

        if (audio) {
            audio.seek(position);
        }
    },
    rate: function (id, rate) {
        const audio = getAudio(id);
        if (audio) {
            audio.rate(rate);
        }
    },
    load: function (id) {
        const audio = getAudio(id);
        if (audio) {
            audio.load();
        }
    },
    unload: function (id) {
        const audio = getAudio(id);
        if (audio) {
            audio.unload();

            audioInstances[id] = null;
            delete instances[id];
        }
    },
    getIsPlaying: function (id) {
        const audio = getAudio(id);

        if (audio) {
            return audio.playing();
        }

        return false;
    },
    getRate: function (id) {
        const audio = getAudio(id);
        if (audio) {
            return audio.rate();
        }

        return 0;
    },
    getCurrentTime: function (id) {
        const audio = getAudio(id);
        if (audio && audio.playing()) {
            const seek = audio.seek();
            return seek === Infinity || isNaN(seek) ? null : seek;
        }
    },
    getTotalTime: function (id) {
        const audio = getAudio(id);

        if (audio) {
            const duration = audio.duration();

            return duration === Infinity || isNaN(duration) ? null : duration;
        }

        return 0;
    },
    destroy: function () {
        Object.keys(audioInstances).forEach(key => {
            try {
                audioInstances[key].unload();
                audioInstances[key] = null;
                delete audioInstances[key];
            }
            catch {
                // move on, no-operation
            }
        });
    }
};

window.globalAudio = {
    mute: function (muted) {
        Howler.mute(muted);
    },
    getCodecs: function () {
        let codecs: Array<string>;

        for (const [key, value] of Object.fromEntries(Howler.codecs)) {
            if (value) {
                codecs.push(key);
            }
        }
        return codecs.sort();
    },
    isCodecSupported: function (extension) {
        return extension ? Howler.codecs[extension.replace(/^x-/, '')] : false;
    }
};

function getAudio(id) {
    return audioInstances[id] ? audioInstances[id].sound : null;
}
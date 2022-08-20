import { Howl, Howler } from '../js/howler.min.js';
import { AudioWindow } from '../js/AudioWindow';

declare const window: AudioWindow;

let audioInstances = {};

window.audio = {
    play: (dotnetReference, options: { sources: any; formats: any; html5: any; loop: any; volume: any; }): number => {

        const sound = new Howl({
            src: options.sources,
            format: options.formats,
            html5: options.html5,
            loop: options.loop,
            volume: options.volume,

            onplay: async (id: number): Promise<void> => {
                let duration = sound.duration(id);

                if (duration === Infinity || isNaN(duration)) {
                    duration = null;
                }

                await dotnetReference.invokeMethodAsync("OnPlayCallback", id, duration);
            },
            onstop: async (id: number): Promise<void> => {
                await dotnetReference.invokeMethodAsync("OnStopCallback", id);
            },
            onpause: async (id: number): Promise<void> => {
                await dotnetReference.invokeMethodAsync("OnPauseCallback", id);
            },
            onrate: async (id: number): Promise<void> => {
                const currentRate = sound.rate();
                await dotnetReference.invokeMethodAsync("OnRateCallback", id, currentRate);
            },
            onend: async (id: number): Promise<void> => {
                await dotnetReference.invokeMethodAsync("OnEndCallback", id);
            },
            onload: async (): Promise<void> => {
                await dotnetReference.invokeMethodAsync("OnLoadCallback");
            },
            onloaderror: async (id: number, error: any): Promise<void> => {
                await dotnetReference.invokeMethodAsync("OnLoadErrorCallback", id, error);
            },
            onplayerror: async (id: number, error: any): Promise<void> => {
                await dotnetReference.invokeMethodAsync("OnPlayErrorCallback", id, error);
            }
        });

        let soundId = sound.play(undefined, false);

        audioInstances[soundId] = {
            sound,
            options
        };

        return soundId;
    },
    playSound: (id: number): void => {
        const audio = getAudio({ id });
        if (audio) {
            audio.play(id);
        }
    },
    stop: (id: number): void => {
        const audio = getAudio({ id });
        if (audio) {
            audio.stop();
        }
    },
    pause: (id: number): void => {
        const audio = getAudio({ id });
        if (audio?.playing()) {
            audio.pause(id);
            return;
        }

        audio.play(id);
    },
    seek: (id: number, position: number): void => {
        const audio = getAudio({ id });

        if (audio) {
            audio.seek(position);
        }
    },
    rate: (id: number, rate: number): void => {
        const audio = getAudio({ id });
        if (audio) {
            audio.rate(rate);
        }
    },
    load: (id: number): void => {
        const audio = getAudio({ id });
        if (audio) {
            audio.load();
        }
    },
    unload: (id: string | number): void => {
        const audio = getAudio({ id });
        if (audio) {
            audio.unload();

            audioInstances[id] = null;
            delete audioInstances[id];
        }
    },
    getIsPlaying: (id: number): boolean => {
        const audio = getAudio({ id });

        if (audio) {
            return audio.playing();
        }

        return false;
    },
    getRate: (id: number): any => {
        const audio = getAudio({ id });
        if (audio) {
            return audio.rate();
        }

        return 0;
    },
    getCurrentTime: (id: number): any => {
        const audio = getAudio({ id });
        if (audio && audio.playing()) {
            const seek = audio.seek();
            return seek === Infinity || isNaN(seek) ? null : seek;
        }
    },
    getTotalTime: (id: number): any => {
        const audio = getAudio({ id });

        if (audio) {
            const duration = audio.duration();

            return duration === Infinity || isNaN(duration) ? null : duration;
        }

        return 0;
    },
    destroy: (): void => {
        Object.keys(audioInstances).forEach(key => {
            try {
                Howler.unload();
            } catch {
                // no-op
            }
        });
    }
};

window.globalAudio = {
    mute: (muted: any): void => {
        Howler.mute(muted);
    },
    getCodecs: (): string[] => {
        let codecs: Array<string> = [];

        for (const [key, value] of Object.entries(Howler.codecs)) {
            if (value) {
                codecs.push(key);
            }
        }
        return codecs.sort();
    },
    isCodecSupported: (extension: string): any => extension ? Howler.codecs[extension.replace(/^x-/, '')] : false
};

function getAudio({ id }: { id: string | number; }): any {
    return audioInstances[id] ? audioInstances[id].sound : null;
}
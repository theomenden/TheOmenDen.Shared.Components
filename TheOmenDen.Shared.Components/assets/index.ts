import { ScriptLoader } from "./dynamicScriptLoader";
import { CaptchaLoader } from "./reCAPTCHAInterop";

declare global {
    interface Window {
        scriptLoader: ScriptLoader;
        captchaLoader: CaptchaLoader;
    }
}

(window as any).scriptLoader = new ScriptLoader();
(window as any).reCaptchaInterop = new CaptchaLoader();
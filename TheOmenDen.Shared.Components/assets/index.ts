import { ScriptLoader } from "./dynamicScriptLoader";
import { ReCaptchaInterop } from "./reCAPTCHAInterop";

document.addEventListener('readystatechange', ev => {
    if (document.readyState === 'complete') {
        const text = document.createElement('p');
        text.innerText = 'Hello from The Omen Den.Shared.Components!';
        document.body.appendChild(text);
    }
});

(window as any).scriptLoader = new ScriptLoader();
(window as any).reCaptchaInterop = new ReCaptchaInterop();
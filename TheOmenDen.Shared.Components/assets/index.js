"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const dynamicScriptLoader_1 = require("./dynamicScriptLoader");
const reCAPTCHAInterop_1 = require("./reCAPTCHAInterop");
document.addEventListener('readystatechange', ev => {
    if (document.readyState === 'complete') {
        const text = document.createElement('p');
        text.innerText = 'Hello from The Omen Den.Shared.Components!';
        document.body.appendChild(text);
    }
});
window.scriptLoader = new dynamicScriptLoader_1.ScriptLoader();
window.reCaptchaInterop = new reCAPTCHAInterop_1.ReCaptchaInterop();
//# sourceMappingURL=index.js.map
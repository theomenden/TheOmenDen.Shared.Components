"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ReCaptchaInterop = void 0;
const ReCaptcha_1 = require("recaptcha-v3/dist/ReCaptcha");
class ReCaptchaInterop {
    async loadCaptchaAsync(siteKey, options) {
        const recaptcha = await (0, ReCaptcha_1.load)(siteKey, options);
        console.info("Captcha loaded", recaptcha);
    }
    async executeCaptchaAsync(action) {
        const recaptcha = (0, ReCaptcha_1.getInstance)();
        return await recaptcha.execute(action);
    }
}
exports.ReCaptchaInterop = ReCaptchaInterop;
//# sourceMappingURL=reCAPTCHAInterop.js.map
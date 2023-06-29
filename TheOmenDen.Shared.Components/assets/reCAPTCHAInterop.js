"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CaptchaLoader = void 0;
const dynamicScriptLoader_1 = require("./dynamicScriptLoader");
class CaptchaLoader {
    _defaultScriptLoadingOptions = {
        isAsync: true,
        isDeferred: true,
        appendedTo: "head",
        maxRetries: 3,
        retryInterval: 100,
        id: "captchaLoader"
    };
    _captchaUrls = new Map([
        ["google", "https://www.google.com/recaptcha/api.js"],
        ["googleEnterprise", "https://www.google.com/recaptcha/enterprise.js"],
        ["recaptcha", "https://www.recaptcha.net/recaptcha/api.js"],
        ["recaptchaEnterprise", "https://www.recaptcha.net/recaptcha/enterprise.js"]
    ]);
    async loadAsync(siteKey, dotNetObjRef, useRecaptchaNet = false, useEnterprise = false, loadingOptions) {
        try {
            const scriptLoader = new dynamicScriptLoader_1.ScriptLoader();
            const pathKey = this.determineCaptchaApi(useEnterprise, useRecaptchaNet);
            const url = `${this._captchaUrls.get(pathKey)}?render=${siteKey}`;
            loadingOptions = { ...this._defaultScriptLoadingOptions, ...loadingOptions };
            await scriptLoader.loadScript(url, loadingOptions);
        }
        catch (error) {
            const errorMessage = error instanceof Error ? error.message : "try again later";
            console.error(`An error occurred while loading captcha.`);
            await dotNetObjRef.invokeMethodAsync("OnCaptchaError", errorMessage);
        }
    }
    async executeAsync(dotNetObjRef, siteKey, action) {
        try {
            const response = await grecaptcha.execute(siteKey, { action: action || "homepage" });
            await dotNetObjRef.invokeMethodAsync("OnCaptchaExecuted", response);
        }
        catch (error) {
            const errorMessage = error instanceof Error ? error.message : "try again later";
            console.error(`An error occurred while executing captcha.`);
            await dotNetObjRef.invokeMethodAsync("OnCaptchaError", errorMessage);
        }
    }
    async renderAsync(siteKey, dotNetObjRef, renderParameters) {
        const transformedParameters = {
            sitekey: siteKey,
            theme: renderParameters?.theme || "dark",
            size: renderParameters?.size || "compact",
            tabindex: renderParameters?.tabindex || 0,
            badge: renderParameters?.badge || "bottomright",
            callback: async (response) => {
                await dotNetObjRef.invokeMethodAsync("OnCaptchaResolved", response);
            },
            "expired-callback": async () => {
                await dotNetObjRef.invokeMethodAsync("OnCaptchaExpired");
            },
            "error-callback": async () => {
                await dotNetObjRef.invokeMethodAsync("OnCaptchaError");
            }
        };
        let widgetId = 0;
        grecaptcha.ready(() => {
            const actualizedContainer = renderParameters?.container || "recaptcha_container";
            widgetId = grecaptcha.render(actualizedContainer, transformedParameters);
        });
        await dotNetObjRef.invokeMethodAsync("OnCaptchaRendered", widgetId);
    }
    async resetAsync(widgetId) {
        grecaptcha.reset(widgetId);
    }
    determineCaptchaApi(useEnterprise, useRecaptchaNet) {
        if (useEnterprise && useRecaptchaNet) {
            return "recaptchaEnterprise";
        }
        if (useEnterprise) {
            return "googleEnterprise";
        }
        if (useRecaptchaNet) {
            return "recaptcha";
        }
        return "google";
    }
}
exports.CaptchaLoader = CaptchaLoader;
//# sourceMappingURL=reCAPTCHAInterop.js.map
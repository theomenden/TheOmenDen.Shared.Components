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
    async loadAsync(url, dotNetObjRef, loadingOptions) {
        try {
            const scriptLoader = new dynamicScriptLoader_1.ScriptLoader();
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
                await dotNetObjRef.invokeMethodAsync("InvokeCallbackAsync", response);
            },
            "expired-callback": async () => {
                await dotNetObjRef.invokeMethodAsync("InvokeExpiredAsync", "Captcha expired.");
            },
            "error-callback": async () => {
                await dotNetObjRef.invokeMethodAsync("InvokeErrorAsync", "An error occurred while rendering captcha.");
            }
        };
        let widgetId = 0;
        grecaptcha.ready(() => {
            const actualizedContainer = renderParameters?.container ?? "recaptcha_container";
            console.info(`Rendering captcha in container: ${actualizedContainer}`);
            widgetId = grecaptcha.render(actualizedContainer, transformedParameters);
            dotNetObjRef.invokeMethodAsync("InvokeCaptchaRenderedAsync", widgetId);
        });
    }
    async resetAsync(widgetId) {
        grecaptcha.reset(widgetId);
    }
    buildQueryParameterString(parameters) {
        if (!parameters || parameters.entries.length === 0) {
            return "";
        }
        const parametersAsObject = Object.fromEntries(parameters);
        const queryParameters = Object.keys(parameters.entries)
            .filter((key) => parametersAsObject[key] !== undefined
            && parametersAsObject[key] !== null)
            .map((key) => `${key}=${parametersAsObject[key]}`)
            .join("&");
        return `&${queryParameters}`;
    }
}
exports.CaptchaLoader = CaptchaLoader;
//# sourceMappingURL=reCAPTCHAInterop.js.map
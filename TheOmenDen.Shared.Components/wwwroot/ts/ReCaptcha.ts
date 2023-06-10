declare var grecaptcha: any;

interface ReCaptchaOptions {
    sitekey: string;
    action: string;
    badge?: "bottomright" | "bottomleft" | "inline";
    size?: "compact" | "normal";
    theme?: "light" | "dark";
}

const loadRecaptcha = async (
    options: ReCaptchaOptions,
    dotNetReference: any
) => {
    const maxRetries = 3;
    let retryCount = 0;

    const isScriptLoaded = () => typeof grecaptcha !== "undefined";

    const loadScript = () => {
        if (isScriptLoaded()) return Promise.resolve();

        return new Promise<void>((resolve, reject) => {
            const script = document.createElement("script");
            script.src = `https://www.google.com/recaptcha/api.js?render=${options.sitekey}`;
            script.async = true;
            script.defer = true;
            script.onload = () => {
                dotNetReference.invokeMethodAsync("LogScriptLoaded", "reCAPTCHA script loaded.");
                resolve();
            };
            script.onerror = () => reject(new Error("Failed to load reCAPTCHA script."));
            document.body.appendChild(script);
        });
    };

    const renderRecaptcha = async () => {
        const widgetId = await grecaptcha.render("recaptcha-container", {
            sitekey: options.sitekey,
            badge: options.badge,
            size: options.size,
            theme: options.theme,
            callback: async (response: string) => {
                dotNetReference.invokeMethodAsync("CallbackMethod", response);
            },
            'expired-callback': async () => {
                dotNetReference.invokeMethodAsync("ExpiredCallbackMethod");
            },
            action: options.action,
        });

        dotNetReference.invokeMethodAsync("WidgetIdCallbackMethod", widgetId);
    };

    const tryLoadRecaptcha = async () => {
        try {
            await loadScript();
            await new Promise<void>((resolve) => setTimeout(resolve, 100));
            await renderRecaptcha();
        } catch (error) {
            retryCount++;
            if (retryCount < maxRetries) {
                await tryLoadRecaptcha();
            } else {
                dotNetReference.invokeMethodAsync("LogError", error.message);
            }
        }
    };

    if (isScriptLoaded()) {
        await renderRecaptcha();
    } else {
        await tryLoadRecaptcha();
    }
};

(window as any).loadRecaptcha = loadRecaptcha;

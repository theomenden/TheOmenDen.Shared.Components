"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ScriptLoader = void 0;
class ScriptLoader {
    _loadedScripts = new Set();
    _targetElement;
    constructor() {
        this._targetElement = document.head;
    }
    async loadScript(url, options = {}) {
        const maxRetries = options.maxRetries || 3;
        const retryDelay = options.retryInterval || 25;
        if (this.isScriptLoaded(url, options.id)) {
            this.logOutResult("Script Already Loaded");
            return;
        }
        let retries = 0;
        while (retries <= maxRetries) {
            try {
                await this.loadScriptAttempt(url, options);
                return;
            }
            catch (error) {
                if (retries >= maxRetries) {
                    this.logOutError("Script Failed To Load after retries");
                    throw error;
                }
                retries++;
                this.logOutError(`Script Failed To Load. Retrying... (${retries}/${maxRetries})`);
                await new Promise((resolve) => setTimeout(resolve, retryDelay));
            }
        }
    }
    async loadScriptAttempt(url, options) {
        return new Promise((resolve, reject) => {
            const script = document.createElement("script");
            script.src = url;
            script.onload = () => {
                this._loadedScripts.add(url + (options.id || ""));
                this.logOutResult("Script Loaded successfully");
                resolve();
            };
            script.onerror = () => {
                reject(new Error("Script failed to load"));
            };
            this.setScriptAttributes(script, options);
            const targetElement = this.getTargetElement(options.appendedTo);
            targetElement.appendChild(script);
        });
    }
    setScriptAttributes(script, options) {
        script.type = "text/javascript";
        script.defer = options.isDeferred || false;
        script.async = options.isAsync || false;
        script.id = options.id || "";
    }
    getTargetElement(appendTo) {
        return appendTo === "head" ? this._targetElement : document.body;
    }
    isScriptLoaded(url, id) {
        const scriptKey = url + (id || "");
        return this._loadedScripts.has(scriptKey);
    }
    logOutResult(message) {
        console.info(`[ScriptLoader]: ${message}`);
    }
    logOutError(error) {
        console.error(`[ScriptLoader]: ${error}`);
    }
}
exports.ScriptLoader = ScriptLoader;
//# sourceMappingURL=dynamicScriptLoader.js.map
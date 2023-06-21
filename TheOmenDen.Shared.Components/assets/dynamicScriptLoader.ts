export interface IScriptLoaderOptions {
    id?: string;
    isAsync?: boolean;
    isDeferred?: boolean;
    appendedTo?: "head" | "body";
    maxRetries?: number;
    retryInterval?: number;
}

interface IScriptLoaderContract {
    loadScript: (url: string, options: IScriptLoaderOptions) => Promise<void>;
}

export class ScriptLoader implements IScriptLoaderContract {
    private _loadedScripts: Set<string> = new Set<string>();
    private _targetElement: HTMLElement;

    constructor() {
        this._targetElement = document.head; // Default to head
    }

    /**
     * Loads a script from a given url
     *
     * @param {string} url - The url of the script to load
     * @param {IScriptLoaderOptions} options - Options for the script loader
     * @returns {Promise<void>}
     *
     * @throws {Error}
     * Thrown when the script fails to load after the max retries
     *
     * @public 
     */
    async loadScript(url: string, options: IScriptLoaderOptions = {}): Promise<void> {
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
            } catch (error) {
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

    private async loadScriptAttempt(url: string, options: IScriptLoaderOptions): Promise<void> {
        return new Promise<void>((resolve, reject) => {
            const script = document.createElement("script") as HTMLScriptElement;
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


    private setScriptAttributes(script: HTMLScriptElement, options: IScriptLoaderOptions): void {
        script.type = "text/javascript";
        script.defer = options.isDeferred || false;
        script.async = options.isAsync || false;
        script.id = options.id || "";
    }

    private getTargetElement(appendTo?: "head" | "body"): HTMLElement {
        return appendTo === "head" ? this._targetElement : document.body;
    }

    private isScriptLoaded(url: string, id?: string): boolean {
        const scriptKey = url + (id || "");
        return this._loadedScripts.has(scriptKey);
    }

    private logOutResult(message: string): void {
        console.info(`[ScriptLoader]: ${message}`);
    }

    private logOutError(error: string): void {
        console.error(`[ScriptLoader]: ${error}`);
    }
}

declare global {
    interface Window {
        scriptLoader: ScriptLoader;
    }
}
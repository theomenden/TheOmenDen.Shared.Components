import { load, getInstance } from "recaptcha-v3/dist/ReCaptcha";
import { IReCaptchaLoaderOptions } from "recaptcha-v3/dist/ReCaptchaLoader";

export class ReCaptchaInterop {

    async loadCaptchaAsync(siteKey: string, options: IReCaptchaLoaderOptions): Promise<void> {
        const recaptcha = await load(siteKey, options);

        console.info("Captcha loaded", recaptcha);
    }

    async executeCaptchaAsync(action: string): Promise<string> {
        const recaptcha = getInstance();

        return await recaptcha.execute(action);
    }
}
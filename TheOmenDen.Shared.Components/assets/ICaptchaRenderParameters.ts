/**
 * The parameters that can be used to render a captcha.
 */
export interface ICaptchaRenderParameters {
    /**
     * The container to render the captcha in. (Default: "recaptcha_container")
     */
    container?: string;
    /**
     * The theme to use for the captcha. (Default: "dark")
     */
    theme?: "dark" | "light";
    /**
     * The size of the captcha to render. (Default: "compact")
     */
    size?: "compact" | "normal" | "invisible";
    /**
     * The tabindex of the captcha to render. (Default: 0)
     */
    tabindex?: number;
    /**
     * The badge location of the captcha to render. (Default: "bottomright")
     */
    badge?: "bottomright" | "bottomleft" | "inline";
}

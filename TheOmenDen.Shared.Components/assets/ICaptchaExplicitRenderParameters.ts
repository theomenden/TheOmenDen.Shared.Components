import { DotNet } from "@microsoft/dotnet-js-interop";

export interface ICaptchaExplicitRenderParameters {
    container?: string;
    theme?: "dark" | "light";
    size?: "compact" | "normal" | "invisible";
    tabindex?: number | 0;
    badge?: "bottomright" | "bottomleft" | "inline";
}
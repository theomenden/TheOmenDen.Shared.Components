import { DotNet } from "@microsoft/dotnet-js-interop";
import { ICaptchaExplicitRenderParameters } from "./ICaptchaExplicitRenderParameters";

export interface IExplicitCaptchaLoaderOptions {
    siteKey: string;
    useEnterprise?: boolean | false;
    useRecaptchaNet?: boolean | false;
    explicitRenderingParameters?: ICaptchaExplicitRenderParameters;
    dotNetObjRef: DotNet.DotNetObject;
}
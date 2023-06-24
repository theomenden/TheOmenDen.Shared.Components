/**
 * A set of properties that can be used to configure the behavior of the script loader.
 */
export interface IScriptLoaderOptions {
    /**
    * An optional unique identifier of the script
    */
    id?: string;
    /**
     * Used to determine if the script should be loaded in a non-blocking manner.
     */
    isAsync?: boolean;
    /**
     * Used to determine if the script should be deferred.
     */
    isDeferred?: boolean;
    /**
     * The location on the document where the script should be loaded.
     */
    appendedTo?: "head" | "body";
    /**
     * The maximum number of attempts to load the script.
     */
    maxRetries?: number;
    /**
     * The interval to wait for the script to complete loading between retries.
     */
    retryInterval?: number;
}

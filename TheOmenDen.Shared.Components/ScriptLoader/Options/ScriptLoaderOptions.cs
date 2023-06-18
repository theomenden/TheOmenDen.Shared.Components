namespace TheOmenDen.Shared.Components.ScriptLoader.Options;
/// <summary>
/// Provides a set of options that can be used to configure how an HTML Script Element will render to load a script.
/// </summary>
public sealed class ScriptLoaderOptions
{
    /// <summary>
    /// The unique id of the script
    /// </summary>
    /// <value>
    /// <code>&lt;script id={Id}&gt;&lt;/script&gt;</code>
    /// </value>
    /// <remarks>
    /// Optional.
    /// </remarks>
    public string Id { get; set; } = String.Empty;
    /// <summary>
    /// Determines if the script should be loaded asynchronously using the <c>async</c> attribute
    /// </summary>
    /// <value>
    /// <c>&lt;script async={IsAsync}&gt;&lt;/script&gt;</c>
    /// </value>
    /// <remarks>
    /// Optional. Defaults to <see langword="false"/>.
    /// </remarks>
    public bool IsAsync { get; set; }
    /// <summary>
    /// Determines if the script should be loaded asynchronously using the <c>defer</c> attribute
    /// </summary>
    /// <value>
    /// <c>&lt;script defer={IsDeferred}&gt;&lt;/script&gt;</c>
    /// </value>
    /// <remarks>
    /// Optional. Defaults to <see langword="false"/>.
    /// </remarks>
    public bool IsDeferred { get; set; }

    /// <summary>
    /// Determines where the script should be appended to in the DOM
    /// </summary>
    /// <value>
    /// Example: <c>AppendedTo = "Head"</c>
    /// <code>
    /// &lt;head&gt;
    /// ...
    /// ...
    /// &lt;script&gt;&lt;/script&gt;
    /// &lt;/head&gt;
    /// </code>
    /// </value>
    /// <remarks>
    /// Defaults to <c>"head"</c>.
    /// </remarks>
    public string AppendedTo { get; set; } = "head";

    /// <summary>
    /// The amount of times to retry loading the script if it fails
    /// </summary>
    /// <remarks>
    /// Defaults to 3.
    /// </remarks>
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// The delay in seconds between each retry
    /// </summary>
    /// <remarks>
    /// Defaults to 25 seconds.
    /// </remarks>
    public int RetryDelayInSeconds { get; set; } = 25;
}

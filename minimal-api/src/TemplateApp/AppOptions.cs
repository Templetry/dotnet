using System.ComponentModel.DataAnnotations;

namespace TemplateApp;

/// <summary>
/// The settings this application actually reads, bound from the `App`
/// section and validated at startup.
///
/// One typed accessor instead of scattered configuration lookups: a missing
/// or nonsensical value fails when the process starts, not on the request
/// that first needed it.
/// </summary>
public sealed class AppOptions
{
    public const string SectionName = "App";

    /// <summary>Which profile these values came from, for /healthz and logs.</summary>
    [Required]
    public string Environment { get; set; } = "development";

    /// <summary>Whether responses may carry detail useful only while developing.</summary>
    public bool VerboseErrors { get; set; }

    /// <summary>Seconds a successful response may be cached by clients.</summary>
    [Range(0, 86_400)]
    public int CacheSeconds { get; set; }
}

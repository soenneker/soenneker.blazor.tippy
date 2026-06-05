using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Tippy.Configuration;

/// <summary>
/// Represents the tippy configuration record.
/// </summary>
public sealed record TippyConfiguration
{
    /// <summary>
    /// Gets or sets content.
    /// </summary>
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    /// <summary>
    /// Gets or sets theme.
    /// </summary>
    [JsonPropertyName("theme")]
    public string? Theme { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether interactive.
    /// </summary>
    [JsonPropertyName("interactive")]
    public bool? Interactive { get; set; }

    /// <summary>
    /// Gets or sets trigger.
    /// </summary>
    [JsonPropertyName("trigger")]
    public string? Trigger { get; set; }

    /// <summary>
    /// Gets or sets placement.
    /// </summary>
    [JsonPropertyName("placement")]
    public string? Placement { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether use cdn.
    /// </summary>
    [JsonPropertyName("useCdn")]
    public bool UseCdn { get; set; } = true;
}

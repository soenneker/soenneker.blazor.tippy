using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Tippy.Configuration;

public sealed record TippyConfiguration
{
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("theme")]
    public string? Theme { get; set; }

    [JsonPropertyName("interactive")]
    public bool? Interactive { get; set; }

    [JsonPropertyName("trigger")]
    public string? Trigger { get; set; }

    [JsonPropertyName("placement")]
    public string? Placement { get; set; }

    [JsonPropertyName("useCdn")]
    public bool UseCdn { get; set; } = true;
}
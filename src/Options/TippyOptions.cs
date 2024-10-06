namespace Soenneker.Blazor.Tippy.Options;

public record TippyOptions
{
    public string? Content { get; set; }

    public string? Theme { get; set; }

    public bool? Interactive { get; set; }

    public string? Trigger { get; set; }

    public string? Placement { get; set; }
}
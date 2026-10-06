using System.Text.Json;
using Soenneker.Blazor.Tippy;

var configuration = JsonSerializer.Deserialize("{}", InteropJsonContext.Default.TippyConfiguration)!;
var payload = JsonSerializer.SerializeToElement(configuration, InteropJsonContext.Default.TippyConfiguration);
Check(payload.ValueKind == JsonValueKind.Object, "configuration wire object");

Console.WriteLine("Trimmed JSON smoke checks passed.");

static void Check(bool condition, string name)
{
    if (!condition) throw new InvalidOperationException(name);
}

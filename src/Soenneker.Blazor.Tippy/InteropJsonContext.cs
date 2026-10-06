using System.Text.Json;
using System.Text.Json.Serialization;
using Soenneker.Blazor.Tippy.Configuration;

namespace Soenneker.Blazor.Tippy;

[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(TippyConfiguration))]
internal partial class InteropJsonContext : JsonSerializerContext;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Constants;

public static class JsonConstants
{
    public static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}

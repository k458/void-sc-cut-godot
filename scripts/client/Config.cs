using System.Text.Json;

namespace voidsccut.scripts.client;

public class Config
{
    public static readonly string ServerUrl = "http://localhost:8090";

    public static JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true
    };

}
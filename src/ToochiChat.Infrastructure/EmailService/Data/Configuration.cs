using Newtonsoft.Json.Linq;

namespace ToochiChat.Infrastructure.EmailService.Data;

internal sealed class Configuration
{
    private readonly JObject _config;

    public string Domain => GetValue<string>("Domain")!;
    public string Controller => GetValue<string>("Controller")!;
    public string Action => GetValue<string>("Action")!;
    public string HtmlFilePath => GetValue<string>("HtmlFilePath")!;
    
    public Configuration(JObject config)
    {
        _config = config;
    }
    
    public T? GetValue<T>(string key) => _config.Value<T>(key);
}
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EnvManager;

public class Env {
    internal string Uuid = "global";
    public string? Name { get; set; }
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true, IncludeFields = true };
    
    [JsonInclude]
    private Dictionary<string, JsonElement> Attributes { get; set; } = new();
    
    public void SetAttribute(string key, object value) {
        if (key == "") throw new ArgumentNullException(nameof(key));
        Attributes[key] = JsonSerializer.SerializeToElement(value, JsonOptions);
        FileManager.SaveEnv(this);
    }

    public T? GetAttribute<T>(string key) => (key == "" ? throw new ArgumentNullException(nameof(key)) : Attributes[key].Deserialize<T>(JsonOptions))!;
}

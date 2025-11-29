using System.Text.Json;
using System.Text.Json.Serialization;

namespace EnvManager;

public class Env {
    internal string Uuid = "global";
    public string? Name { get; set; }
    
    [JsonInclude]
    private Dictionary<string, string> Attributes { get; set; } = new();
    
    public void SetAttribute(string key, object value)
    {
        if (key == "") throw new ArgumentNullException(nameof(key));
        Attributes[key] = JsonSerializer.Serialize(value);
        Console.WriteLine($"{key}: {JsonSerializer.Serialize(value)}");
        FileManager.SaveEnv(this);
    }

    public T GetAttribute<T>(string key) {
        return (key == "" ? throw new ArgumentNullException(nameof(key)) : JsonSerializer.Deserialize<T>(Attributes[key]))!;
    }
}

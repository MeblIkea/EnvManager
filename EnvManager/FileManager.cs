using System.Text.Json;
using System.Text;

namespace EnvManager;

internal static class FileManager {
    private static readonly object Lock = new();
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true, IncludeFields = false };
    
    private static string GetEnvPath() {
        string path = Path.Combine(Path.GetDirectoryName(AppContext.BaseDirectory)!, ".csenv");
        if (File.Exists(path)) return path;
        FileStream fs = File.Create(path);
        byte[] info = new UTF8Encoding(false).GetBytes("{\n}"); 
        fs.Write(info, 0, info.Length);
        fs.Close();
        return path;
    }
    
    internal static Env GetEnv(string uuid) {
        Dictionary<string, Env> envs = GetEnvs();
        if (envs.TryGetValue(uuid, out Env? value)) {
            value.Uuid =  uuid;
            return value;
        }
        Env newEnv = new();
        envs[uuid] = newEnv;
        newEnv.Uuid = uuid;
        string json = JsonSerializer.Serialize(envs, JsonOptions);
        lock (Lock) File.WriteAllText(GetEnvPath(), json);
        return newEnv;
    }

    internal static void SaveEnv(Env env) {
        Dictionary<string, Env> envs = GetEnvs();
        envs[env.Uuid] = env;
        SaveEnvs(envs);
    }

    private static Dictionary<string, Env> GetEnvs() {
        string fileContent;
        lock (Lock) fileContent = File.ReadAllText(GetEnvPath());
        return JsonSerializer.Deserialize<Dictionary<string, Env>>(fileContent)!;
    }
    private static void SaveEnvs(Dictionary<string, Env> envs) {
        string json = JsonSerializer.Serialize(envs, JsonOptions);
        lock (Lock) File.WriteAllText(GetEnvPath(), json);
    }
}

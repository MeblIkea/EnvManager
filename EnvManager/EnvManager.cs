namespace EnvManager;

public static class EnvManager {
    public static Env GetEnv(string uuid="global", string name="") {
        if (uuid == "global")
            name = "Global Attributes";
        Env env = FileManager.GetEnv(uuid);
        if (name == "" || name == env.Name) return env;
        env.Name = name;
        FileManager.SaveEnv(env);
        return env;
    }
}

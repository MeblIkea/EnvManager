namespace EnvManager {
    public static class EnvironmentManager {
        /// <summary>
        /// Function to get (or create) an environment in the .csenv file.
        /// <para>This function is to set/get on the global environment.</para>
        /// </summary>
        /// <returns>Environment of your variables</returns>
        public static Env GetGlobalEnv() {
            return GetEnv("global");
        }
    
        /// <summary>
        /// Function to get (or create) an environment in the .csenv file.
        /// </summary>
        /// <param name="uuid">Key of your environment</param>
        /// <param name="name">Description of your environment (For JSON)</param>
        /// <returns></returns>
        public static Env GetEnv(string uuid, string name="") {
            if (uuid == "global")
                name = "Global Attributes";
            Env env = FileManager.GetEnv(uuid);
            if (name == "" || name == env.Name) return env;
            env.Name = name;
            FileManager.SaveEnv(env);
            return env;
        }
    }
}

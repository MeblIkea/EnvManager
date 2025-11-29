using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EnvManager {
    public class Env {
        internal string Uuid = "global";
        public string Name { get; set; }
        
        [JsonProperty]
        private Dictionary<string, JToken> Attributes { get; set; } = new Dictionary<string, JToken>();
    
        public void SetAttribute(string key, object value) {
            if (key == "") throw new ArgumentNullException(nameof(key));
            Attributes[key] = JToken.FromObject(value);
            FileManager.SaveEnv(this);
        }

        public T GetAttribute<T>(string key) {
            if (Attributes.ContainsKey(key)) 
                return Attributes[key].ToObject<T>();
            SetAttribute(key, "");
            throw new KeyNotFoundException(key);
        }
    }
}

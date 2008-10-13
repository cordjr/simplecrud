using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCrud.Core {
    public class ConfigManager {
        private readonly IDictionary<Type, SimpleConfig> mapConfig;
        private static ConfigManager instance;

        public static ConfigManager GetInstance() {
            if (instance == null) {
                instance = new ConfigManager();
            }
            return instance;
        }
        private  ConfigManager() {
            mapConfig = new Dictionary<Type, SimpleConfig>();
        }
       
        public void AddConfig(Type type, SimpleConfig config){
            mapConfig.Add(new KeyValuePair<Type, SimpleConfig>(type,config));
        }

        public SimpleConfig GetConfig(Type type) {
            return mapConfig[type];
        }


    }
}

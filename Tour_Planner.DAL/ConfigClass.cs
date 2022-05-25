using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tour_Planner.DAL
{
    public sealed class ConfigClass
    {
        private static ConfigClass instance = null;

        private ConfigClass()
        {
        }

        public static ConfigClass Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigClass();
                }
                return instance;
            }
        }

        public string GetConnectionString()
        {
            var file = File.ReadAllText($"..\\..\\..\\config.json");

            JObject text = JsonConvert.DeserializeObject<JObject>(file);    // JObject oder dynamic(ohne ToString())
            string connect = text["database"]["connection"].ToString();

            return connect;
        }

        public string GetKeyFromConfig()
        {
            var file = File.ReadAllText($"..\\..\\..\\config.json");
            JObject text = JsonConvert.DeserializeObject<JObject>(file);
            string key = text["key"].ToString();
            return key;
        }

        public string GetImageSourceFromConfig()
        {
            var file = File.ReadAllText($"..\\..\\..\\config.json");
            JObject text = JsonConvert.DeserializeObject<JObject>(file);
            string source = text["imageSource"].ToString();
            return source;
        }
    }
}

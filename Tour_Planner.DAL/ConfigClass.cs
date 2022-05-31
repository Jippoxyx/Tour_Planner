using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tour_Planner.Logging;

namespace Tour_Planner.DAL
{
    public sealed class ConfigClass
    {
        private static ConfigClass instance = null;
        ILoggerWrapper _loggerWrapper = LoggerFactory.GetLogger();

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
            string connect = null;
            try
            {
                var file = File.ReadAllText($"..\\..\\..\\config.json");

                JObject text = JsonConvert.DeserializeObject<JObject>(file);    // JObject oder dynamic(ohne ToString())
                connect = text["database"]["connection"].ToString();
            }
            catch(Exception ex)
            {
                _loggerWrapper.Warn("Cant find [database] in config");
            }
            
            return connect;
        }

        public string GetKeyFromConfig()
        {
            string key = null;
            try
            {
                var file = File.ReadAllText($"..\\..\\..\\config.json");
                JObject text = JsonConvert.DeserializeObject<JObject>(file);
                 key = text["key"].ToString();

            }
            catch (Exception ex)
            {
                _loggerWrapper.Warn("Cant find [key] in config");
            }
            
            return key;
        }

        public string GetImageSourceFromConfig()
        {
            string source = null;
            try
            {
                var file = File.ReadAllText($"..\\..\\..\\config.json");
                JObject text = JsonConvert.DeserializeObject<JObject>(file);
                source = text["imageSource"].ToString();
            }
            catch(Exception ex)
            {
                _loggerWrapper.Warn("Cant find [imageSource] in config");
            }
            
            return source;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.DAL;

namespace Tour_Planner.BL.Service
{
    public class ConfigService
    {
        public ConfigClass GetSingletonInstance()
        {
            return ConfigClass.Instance;
        }
    }
}

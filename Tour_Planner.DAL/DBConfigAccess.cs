using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Tour_Planner.DAL
{
    // Design-Pattern - Singleton
    public class DBConfigAccess
    {
        private static DBConfigAccess instance;


        public static DBConfigAccess Instance()
        {
            return instance ??= new DBConfigAccess();   // if Instance == NULL -> Create DBCA() else return instance
        }
    }
}

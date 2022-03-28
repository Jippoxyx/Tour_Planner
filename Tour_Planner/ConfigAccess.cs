using Newtonsoft.Json;  // added
using Newtonsoft.Json.Linq; // added
using Npgsql;               // added
using System;
using System.Collections.Generic;
using System.IO;    // added
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.PL
{
    class ConfigAccess
    {
        public ConfigAccess()
        {
            NpgsqlConnection con = Connect();
            Test(con);
        }


        public static NpgsqlConnection Connect()
        {
            var file = File.ReadAllText("C:\\Users\\Hawryluk\\Desktop\\Tour_Planner-main\\Tour_Planner-main\\Tour_Planner\\config.json");           // pfad anpassen -> wohin mim config?

            JObject text = JsonConvert.DeserializeObject<JObject>(file);    // JObject oder dynamic(ohne ToString())
            string connect = text["database"]["connection"].ToString();

            return new NpgsqlConnection(connect);
        }


        public void Test(NpgsqlConnection con)
        {
            con.Open();

            string query = $"INSERT INTO public.\"TestDB_TP\" (name, alter) values('Test', 31);";

            NpgsqlCommand command = new NpgsqlCommand(query, con);
            //Int64 count = (Int64)command.ExecuteScalar();
            NpgsqlDataReader read = command.ExecuteReader();

            /*while (read.Read())
            {
                Console.WriteLine($"\nTest SQL: {read[0]}\t{read[1]}\n");

            }*/
            con.Close();

        }
    }
}

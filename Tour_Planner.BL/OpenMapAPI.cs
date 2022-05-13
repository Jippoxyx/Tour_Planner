using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tour_Planner.Models;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Tour_Planner.BL
{
    public class OpenMapAPI
    {
        ParseResponse _parseResponse = new ParseResponse();

        public async Task<Tour> GetTour(string from, string to)
        {
            var tour = new Tour() { Id = Guid.NewGuid() };

            //key should be from config file
            var url = $"http://open.mapquestapi.com/directions/v2/route?key={GetKeyFromConfig()}&from={from}&to={to}";
            using var client = new HttpClient();

            var response = await client.GetStringAsync(url);
            Console.WriteLine(response);

            tour = _parseResponse.ParseTourFromServer(response);
            tour.RouteImagePath = await GetTourImage(tour.Session, tour.BoundingBox);

            return tour;
        }

        private static string GetKeyFromConfig()
        {
            var file = File.ReadAllText($"..\\..\\..\\config.json");
            JObject text = JsonConvert.DeserializeObject<JObject>(file);
            string key = text["key"].ToString();
            return key;
        }

        private static string GetImageSourceFromConfig()
        {
            var file = File.ReadAllText($"..\\..\\..\\config.json");
            JObject text = JsonConvert.DeserializeObject<JObject>(file);
            string source = text["imageSource"].ToString();
            return source;
        }

        public async Task<string> GetTourImage(string session, string boundingBox)
        {
            //key should be from config file
            var url = $"http://mapquestapi.com/staticmap/v5/map?key={GetKeyFromConfig()}&size=300,640&session={session}&boundingBox={boundingBox}";
            using var client = new HttpClient();

            var response = await client.GetByteArrayAsync(url);

            byte[] bitmap = response;


            string path = GetImageSourceFromConfig();
            path += $"{Guid.NewGuid()}.jpeg";
            using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(bitmap)))
            {
                image.Save(path, ImageFormat.Jpeg);  // Or Png
            }

            return path;
        }
    }
}

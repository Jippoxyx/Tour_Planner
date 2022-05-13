using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tour_Planner.Models;

namespace Tour_Planner.BL
{
    public class OpenMapAPI
    {
        ParseResponse _parseResponse = new ParseResponse();

        public async Task<Tour> GetTour(string from, string to)
        {
            var tour = new Tour() { Id = Guid.NewGuid() };

            var json = JsonConvert.SerializeObject(tour);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            //key should be from config file
            var url = $"http://open.mapquestapi.com/directions/v2/route?key=GFOcOZ5wey1dHS2iZGavpmEi7Ret0BZn&from=\"{from}\"&to=\"{to}\"";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);
            //Console.WriteLine(response);

            string result = await response.Content.ReadAsStringAsync();

            return _parseResponse.ParseTourFromServer(result);
        }
    }
}

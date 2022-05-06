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
        public async Task<Tour> GetTour(string from, string to)
        {
            var tour = new Tour();

            var json = JsonConvert.SerializeObject(tour);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://open.mapquestapi.com/directions/v2/route?key=GFOcOZ5wey1dHS2iZGavpmEi7Ret0BZn&from=\"{from}\"&to=\"{to}\"";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            return tour;
        }
    }
}

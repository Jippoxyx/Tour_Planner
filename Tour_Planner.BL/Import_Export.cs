using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tour_Planner.BL.Exceptions;
using Tour_Planner.Models;

namespace Tour_Planner.BL
{
    public class Import_Export
    {

        public void exportTour(Tour tour)
        {
            try
            {
                string json = JsonSerializer.Serialize(tour);
                Console.WriteLine(json);
                File.WriteAllText(tour.Title + ".json", json);
            }
            catch (Exception)
            {
                throw new Export_Exception("Could not export Tour");
            }
        }

        public Tour importTour(string jsonObject)
        {
            try
            {
                Tour _tour = JsonSerializer.Deserialize<Tour>(jsonObject);
                return _tour;
            }
            catch (Exception)
            {
                throw new Import_Exception("Cound not import Tour");
            }
        }
    }
}

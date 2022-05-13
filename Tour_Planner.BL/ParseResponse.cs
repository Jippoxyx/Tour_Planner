using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Models;

namespace Tour_Planner.BL
{
    public class ParseResponse
    {
        

        public Tour ParseTourFromServer(string tourInfo)
        {
            Tour tourObj = new Tour() { Id = Guid.NewGuid() };
            JObject json = JObject.Parse(tourInfo);
           // Console.WriteLine(json);
            tourObj.TourDistance = json["route"]["distance"].ToString();
            tourObj.EstimatedTime = json["route"]["time"].ToString();
            
            return tourObj;
        }
    }
}

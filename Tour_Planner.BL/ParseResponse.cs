using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Logging;
using Tour_Planner.Models;

namespace Tour_Planner.BL
{
    public class ParseResponse
    {
        ILoggerWrapper _loggerWrapper = LoggerFactory.GetLogger();

        public Tour ParseTourFromServer(string tourInfo)
        {
            Tour tourObj = new Tour() { Id = Guid.NewGuid() };
            try
            {               
                JObject json = JObject.Parse(tourInfo);
                tourObj.TourDistance = json["route"]["distance"].ToString();
                tourObj.EstimatedTime = json["route"]["time"].ToString();
                tourObj.Session = json["route"]["sessionId"].ToString();
                tourObj.BoundingBox += json["route"]["boundingBox"]
                    ["ul"]["lat"].ToString();
                tourObj.BoundingBox += ",";
                tourObj.BoundingBox += json["route"]["boundingBox"]
                    ["ul"]["lng"].ToString();
                tourObj.BoundingBox += ",";
                tourObj.BoundingBox += json["route"]["boundingBox"]
                    ["lr"]["lat"].ToString();
                tourObj.BoundingBox += ",";
                tourObj.BoundingBox += json["route"]["boundingBox"]
                    ["lr"]["lng"].ToString();
                Console.WriteLine(tourObj.BoundingBox);
            }
            catch (NullReferenceException)
            {
                _loggerWrapper.Warn("Server didnt return a Tour - expected Object == null");
            }
            return tourObj;
        }
    }
}

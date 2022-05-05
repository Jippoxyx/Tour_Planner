using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.Models
{
    public class Tour
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Desciption { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string TransportType { get; set; }
        public string TourDistance { get; set; }
        public string EstimatedTime { get; set; }
        public string RouteInformation{ get; set; }

        public List<TourLog> Logs { get; set; } = new List<TourLog>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.Models
{
    public class TourLog
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public int Time { get; set; }
        public string Comment { get; set; }
        public int Difficulty { get; set; }
        public int TotalTime { get; set; }
        public int Rating { get; set; }
    }
}

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

        public List<TourLog> Logs { get; set; }
    }
}

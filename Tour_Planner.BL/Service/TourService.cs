using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.DAL;
using Tour_Planner.Models;

namespace Tour_Planner.BL.Service
{
    public class TourService 
    {
        private readonly ITourManager _tourManager;

        public TourService(ITourManager tourManager)
        {
            _tourManager = tourManager;        
        }

        public List<Tour> GetData()
        {
            return _tourManager.GetTourData();
        }
    }
}

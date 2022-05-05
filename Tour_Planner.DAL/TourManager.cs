using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Models;

namespace Tour_Planner.DAL
{
    class TourManager : ITourManager
    {
        public TourLog CreateLog()
        {
            throw new NotImplementedException();
        }

        public Tour CreateTour()
        {
            throw new NotImplementedException();
        }

        public void DeleteAllTours()
        {
            throw new NotImplementedException();
        }

        public void DeleteTour(Tour tour)
        {
            throw new NotImplementedException();
        }

        public void DeleteTourLog(TourLog log)
        {
            throw new NotImplementedException();
        }

        public List<Tour> GetTourData()
        {
            throw new NotImplementedException();
        }

        public List<TourLog> GetTourLogData(Tour tour)
        {
            throw new NotImplementedException();
        }
    }
}

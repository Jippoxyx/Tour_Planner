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

        public void AddTour(Tour tour)
        {
            //save in db 
            _tourManager.CreateTour(tour);
        }

        public void DeleteSelectedTour(Tour tour)
        {
            _tourManager.DeleteTour(tour);
        }
        public void DeleteSelectedTourLog(Tour tour,TourLog log)
        {
            _tourManager.DeleteTourLog(tour, log);
        }

        public List<TourLog> GetLogData(Tour tour)
        {
            return _tourManager.GetTourLogData(tour);
        }

        public void AddLog(Tour tour, TourLog log)
        {
            _tourManager.CreateLog(tour, log);
        }       

        public void DeleteAllTours()
        {
            _tourManager.DeleteAllTours();
        }
    }
}

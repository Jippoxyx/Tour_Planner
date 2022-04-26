using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.DAL;
using Tour_Planner.Models;

namespace Tour_Planner.BL
{
    public class TourFactory : ITourFactory
    {
        private readonly ITourDAO tourDAO = new TourDAO();

        public void AddLog(Tour tour)
        {
            throw new NotImplementedException();
        }

        public void AddTour(Tour tour)
        {
            tourDAO.DB_AddTour(tour);
            //throw new NotImplementedException();
        }

        public void DeleteLog(Tour tour)
        {
            throw new NotImplementedException();
        }

        public void DeleteTour(Tour tour)
        {
            throw new NotImplementedException();
        }

        public void GetTour()
        {
            throw new NotImplementedException();
        }

        public void SearchTour(string search)
        {
            throw new NotImplementedException();
        }

        public void UpdateLog(Tour tour)
        {
            throw new NotImplementedException();
        }

        public void UpdateTour(Tour tour)
        {
            throw new NotImplementedException();
        }
    }
}

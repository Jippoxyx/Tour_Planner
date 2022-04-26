using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Models;

namespace Tour_Planner.DAL
{
    public interface ITourManager
    {
        void AddTour(Tour tour);
        void DeleteTour(Tour tour);
        void UpdateTours(Tour tour);
        List<Tour> GetTourData();

    }
}

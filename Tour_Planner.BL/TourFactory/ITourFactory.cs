using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Models;

namespace Tour_Planner.BL
{
    public interface ITourFactory
    {
        /*
         * TODO: ITour?
         */

        void AddTour(Tour tour);
        void UpdateTour(Tour tour);
        void DeleteTour(Tour tour);

        void GetTour();
        void SearchTour(string search);

        void AddLog(Tour tour);
        void UpdateLog(Tour tour);
        void DeleteLog(Tour tour);

    }
}

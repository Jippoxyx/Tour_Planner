using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Models;

namespace Tour_Planner.DAL
{
    public interface ITourDAO
    {
        void DB_AddTour(Tour tour);
        void DB_UpdateTour(Tour tour);
        void DB_DeleteTour(Tour tour);

        void DB_GetAllTours();      // Enum / Liste ?
        void DB_SearchTour(string search);

        void DB_AddLog(Tour tour);
        void DB_UpdateLog(Tour tour);
        void DB_DeleteLog(Tour tour);
    }
}

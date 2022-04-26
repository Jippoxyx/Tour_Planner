using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.DAL
{
    public interface ITourManager
    {
        void AddTour();
        void DeleteTour();
        void DeleteAllTours();
        void UpdateTours();

    }
}

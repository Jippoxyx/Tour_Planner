using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tour_Planner.Model;
using Tour_Planner.ViewModels.Utility;

namespace Tour_Planner.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        TourViewModel tour;

        public MainViewModel(MenuViewModel menu, 
            TourViewModel tour, 
            TourDetailsViewModel tourDetails, 
            SearchBarViewModel search) 
        {
            this.tour = tour;
            tour.tours += (_, t) =>
            {
                AddTourExecute(t);
            };
        }

        private void DeleteTourExecute()
        {
            tour.TourData.Clear();
        }

        private void AddTourExecute(Tour t)
        {
            tour.TourData.Add(t);
        }
    }
}

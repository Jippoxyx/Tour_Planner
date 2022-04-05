using System;
using System.Collections.Generic;
using System.Linq;
using Tour_Planner.Models;
using Tour_Planner.Models;
using Tour_Planner.ViewModels.Utility;

namespace Tour_Planner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        TourViewModel tour;
        TourDetailsViewModel tourDetailsViewModel;

        public MainViewModel(MenuViewModel menu, 
            TourViewModel tour, 
            TourDetailsViewModel tourDetails, 
            SearchBarViewModel search)
        {
            this.tour = tour;
            this.tourDetailsViewModel = tourDetails;
            SetUpTourView();
        }

        private void SetUpTourView()
        {
            Add_AddTourEvent();
            Add_DeleteTourEvent();
            Add_DeleteAllEvent();
            Add_DisplayTourDetails();
        }

        private void Add_DisplayTourDetails()
        {
            tour.displayTourDetails += (_, t) =>
            {
                tourDetailsViewModel.Tour = tour.SelectedItem;
            };
          
        }

        private void Add_DeleteTourEvent()
        {
            tour.deleteTourEvent += (_, t) =>
            {
                DeleteTourExecute(t);
            };
        }
        

        private void Add_DeleteAllEvent()
        {
            tour.deleteAllToursEvent += (_, t) =>
            {
                DeleteAllToursExecute();
            };
        }

        private void Add_AddTourEvent()
        {
            tour.addTourEvent += (_, t) =>
            {
                AddTourExecute(t);
            };
        }

        private void DeleteAllToursExecute()
        {
            tour.TourData.Clear();
        }
        
        private void DeleteTourExecute(Tour t)
        {
            if(t != null)
            {
                List<Tour> items = tour.TourData.Where(x => x.Id == t.Id).ToList();
                foreach (Tour tou in items)
                {
                    tour.TourData.Remove(tou);
                }
            }           
        }

        private void AddTourExecute(Tour t)
        {                   
            tour.TourData.Add(t);
        }
    }
}

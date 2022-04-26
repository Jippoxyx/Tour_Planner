using System;
using System.Collections.Generic;
using System.Linq;
using Tour_Planner.Models;
using Tour_Planner.ViewModels.Utility;
using Tour_Planner.BL.Service;
using Tour_Planner.Bl;

namespace Tour_Planner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        TourService _tourService;
        TourViewModel _tour;
        TourDetailsViewModel _tourDetailsViewModel;

        public MainViewModel(MenuViewModel menu, 
            TourViewModel tour, 
            TourDetailsViewModel tourDetails, 
            SearchBarViewModel search)
        {
            _tourService = new IoCContainerConfig().tourService;           
            this._tour = tour;
            this._tourDetailsViewModel = tourDetails;
            SetUpTourView();
        }

        private void loadData()
        {
            foreach (var tour in _tourService.GetData())
            {
                _tour.TourData.Add(tour);
            }          
        }

        private void SetUpTourView()
        {
            Add_AddTourEvent();
            Add_DeleteTourEvent();
            Add_DeleteAllEvent();
            Add_DisplayTourDetails();
            loadData();
        }

        private void Add_DisplayTourDetails()
        {
            _tour.displayTourDetails += (_, t) =>
            {
                _tourDetailsViewModel.Tour = _tour.SelectedItem;
            };
          
        }

        private void Add_DeleteTourEvent()
        {
            _tour.deleteTourEvent += (_, t) =>
            {
                DeleteTourExecute(t);
            };
        }
        

        private void Add_DeleteAllEvent()
        {
            _tour.deleteAllToursEvent += (_, t) =>
            {
                DeleteAllToursExecute();
            };
        }

        private void Add_AddTourEvent()
        {
            _tour.addTourEvent += (_, t) =>
            {
                AddTourExecute(t);
            };
        }

        private void DeleteAllToursExecute()
        {
            _tour.TourData.Clear();
        }
        
        private void DeleteTourExecute(Tour t)
        {
            if(t != null)
            {
                List<Tour> items = _tour.TourData.Where(x => x.Id == t.Id).ToList();
                foreach (Tour tou in items)
                {
                    _tour.TourData.Remove(tou);
                }
            }           
        }

        private void AddTourExecute(Tour t)
        {                   
            _tour.TourData.Add(t);
        }
    }
}

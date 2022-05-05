using System;
using System.Collections.Generic;
using System.Linq;
using Tour_Planner.Models;
using Tour_Planner.ViewModels.Utility;
using Tour_Planner.BL.Service;
using Tour_Planner.BL;
using System.Collections.ObjectModel;

namespace Tour_Planner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        BL.Service.TourService _tourService;
        TourViewModel _tour;
        TourDetailsViewModel _tourDetailsViewModel;
        SearchBarViewModel _searchVM;
        MenuViewModel _menu;

        public MainViewModel(MenuViewModel menu, 
            TourViewModel tour, 
            TourDetailsViewModel tourDetails, 
            SearchBarViewModel search)
        {
            _tourService = new IoCContainerConfig().tourService;           
            this._tour = tour;
            SetUpTourView();
            this._tourDetailsViewModel = tourDetails;
            SetUpLogs();
            this._searchVM = search;
            SetUpSearch();
            this._menu = menu;
            SetUpMenu();
        }

        private void SetUpMenu()
        {
            Add_DeleteAllButton();
        }

        private void Add_DeleteAllButton()
        {
            _menu.deleteAllToursEvent += (_, t) =>
            {              
                _tourService.DeleteAllTours();
               _tour.TourData.Clear();
                loadData();
                _tourDetailsViewModel.TourLogData.Clear();
                loadLogData();
            };
        }
        private void SetUpTourView()
        {
            Add_AddTourEvent();
            Add_DeleteTourEvent();
            Add_DisplayTourDetails();
            loadData();           
        }

        private void loadLogData()
        {
            foreach (var log in _tourService.GetLogData(_tour.SelectedItem))
            {
                _tourDetailsViewModel._tourLogData.Add(log);
            }
        }

        private void loadData()
        {
            foreach (var tour in _tourService.GetData())
            {
                _tour.TourData.Add(tour);
            }
        }

        private void SetUpSearch()
        {
            Add_SearchTours();
        }

        void Add_SearchTours()
        {
            _searchVM.SearchBoxChanged += (_, searchTours) =>
            {
                var result = string.Join("\n", $"Dummy: {searchTours}");
                this._searchVM.DisplayResult(result);
            };
        }

        private void Add_DisplayTourDetails()
        {
            _tour.displayTourDetails += (_, t) =>
            {
                _tourDetailsViewModel.Tour = _tour.SelectedItem;
                if (_tour.SelectedItem != null)
                {
                    _tourDetailsViewModel.TourLogData
                = new ObservableCollection<TourLog>(_tour.SelectedItem.Logs);
                }                            
            };         
        }

        private void Add_DeleteTourEvent()
        {
            _tour.deleteTourEvent += (_, t) =>
            {
                _tourService.DeleteSelectedTour(t);
                _tour.TourData.Clear();
                loadData();
                _tourDetailsViewModel.TourLogData.Clear();
                loadLogData();
            };
        }      

        private void Add_AddTourEvent()
        {
            _tour.addTourEvent += (_, t) =>
            {
                t = _tourService.AddTour();
                _tour.TourData.Add(t);
            };
        }

        private void SetUpLogs()
        {
            loadLogData();
            Add_AddLogEvent();
            Add_DeleteLogEvent();
        }

        private void Add_AddLogEvent()
        {
            _tourDetailsViewModel.addLogEvent += (_, l) =>
            {
                if(_tour.SelectedItem != null)
                {
                    l = _tourService.AddLog(_tour.SelectedItem);
                    _tourDetailsViewModel.TourLogData.Add(l);
                }              
            };
        }

        private void Add_DeleteLogEvent()
        {
            _tourDetailsViewModel.deleteLogEvent += (_, l) =>
            {
                if(_tourDetailsViewModel.SelectedLog != null)
                {
                    _tourService.DeleteSelectedTourLog(_tour.SelectedItem ,l);
                    _tourDetailsViewModel.TourLogData.Clear();
                    loadLogData();
                }               
            };
        }      
    }
}

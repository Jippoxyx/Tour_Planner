using System;
using System.Collections.Generic;
using System.Linq;
using Tour_Planner.Models;
using Tour_Planner.ViewModels.Utility;
using Tour_Planner.BL.Service;
using Tour_Planner.BL;
using System.Collections.ObjectModel;
using Tour_Planner.PL.View;
using System.ComponentModel;

namespace Tour_Planner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        BL.Service.TourService _tourService;
        TourViewModel _tour;
        TourDetailsViewModel _tourDetailsViewModel;
        SearchBarViewModel _searchVM;
        MenuViewModel _menu;

        TourInfoViewModel _tourInfoViewModel = new TourInfoViewModel();
        TourInfoView _tourInfoView = new TourInfoView();
        OpenMapAPI _openMapAPI = new OpenMapAPI();

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
            _tourInfoView.DataContext = _tourInfoViewModel;
        }

        private void SetUpMenu()
        {
            Add_DeleteAllButton();
        }

        private void Add_DeleteAllButton()
        {
            _menu.deleteAllToursEvent += (_, e) =>
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

            Add_DisplayFromToWindow();
            Add_UserInputCreateTour();
        }

        private void Add_UserInputCreateTour()
        {
            _tourInfoViewModel.confirmTourInfo += async (_, t) =>
            {
                Tour tour = await _openMapAPI.GetTour(_tourInfoViewModel.From, _tourInfoViewModel.To);
               //Console.WriteLine($"{tour.EstimatedTime} {tour.TourDistance}");
               _tourService.AddTour(tour);
                _tour.TourData.Add(tour);
            };
        }

        private void loadLogData()
        {
            if (_tour.SelectedItem != null)
            {
                foreach (var log in _tourService.GetLogData(_tour.SelectedItem))
                {
                    _tourDetailsViewModel.TourLogData.Add(log);
                }
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
                _tourDetailsViewModel.DisplayImage = _tour.SelectedItem.RouteImagePath;
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
                _tourService.AddTour(t);
                _tour.TourData.Add(t);
            };
        }

        private void Add_DisplayFromToWindow()
        {
            _tour.displayGetTourWindow += (_, e) =>
            {
                _tourInfoView.Show();
            };
        }

        private void SetUpLogs()
        {
            loadLogData();
            Add_DisplayTourLogDetails();
            Add_AddLogEvent();
            Add_DeleteLogEvent();
        }

        private void Add_DisplayTourLogDetails()
        {
            _tourDetailsViewModel.displayTourLogDetails += (_, t) =>
            {
                if (_tour.SelectedItem != null && _tourDetailsViewModel.SelectedLog != null)
                {
                    _tourDetailsViewModel.TourLog = _tourDetailsViewModel.SelectedLog;
                }
            };
        }

        private void Add_AddLogEvent()
        {
            _tourDetailsViewModel.addLogEvent += (_, l) =>
            {
                if (_tour.SelectedItem != null)
                {
                    _tourService.AddLog(_tour.SelectedItem, l);
                    _tourDetailsViewModel.TourLogData.Add(l);
                }
            };
        }

        private void Add_DeleteLogEvent()
        {
            _tourDetailsViewModel.deleteLogEvent += (_, l) =>
            {
                if (_tourDetailsViewModel.SelectedLog != null)
                {
                    _tourService.DeleteSelectedTourLog(_tour.SelectedItem, l);
                    _tourDetailsViewModel.TourLogData.Clear();
                    loadLogData();
                }
            };
        }
    }
}

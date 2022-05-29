using System;
using Tour_Planner.Models;
using Tour_Planner.ViewModels.Utility;
using Tour_Planner.BL.Service;
using Tour_Planner.BL;
using System.Collections.ObjectModel;
using Tour_Planner.PL.View;
using System.Windows;
using Tour_Planner.Logging;
using Tour_Planner.BL.Tour_Documentation;
using Tour_Planner.PL.ViewModels;
using Microsoft.Win32;
using System.IO;
using System.Collections.Generic;
using Tour_Planner.BL.Exceptions;

namespace Tour_Planner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        TourService _tourService;
        TourViewModel _tour;
        TourDetailsViewModel _tourDetailsViewModel;
        SearchBarViewModel _searchVM;
        MenuViewModel _menu;

        TourInfoView _tourInfoView = new TourInfoView();
        TourInfoViewModel _tourInfoViewModel = new TourInfoViewModel();    

        ImportTourView _importView = new ImportTourView();
        ImportTourViewModel _importTourVM = new ImportTourViewModel();

        OpenMapAPI _openMapAPI = new OpenMapAPI();
        ILoggerWrapper _loggerWrapper = LoggerFactory.GetLogger();
        Reporting _report = new Reporting();       

        public MainViewModel(MenuViewModel menu,
            TourViewModel tour,
            TourDetailsViewModel tourDetails,
            SearchBarViewModel search)
        {
            _tourService = new IoCContainerConfig().tourService;
            this._tour = tour;
            this._tourDetailsViewModel = tourDetails;
            this._searchVM = search;
            this._menu = menu;

            SetUpTourView();           
            SetUpLogs();           
            SetUpSearch();           
            SetUpMenu();

            _tourInfoView.DataContext = _tourInfoViewModel;
            _importView.DataContext = _importTourVM;
        }

        private void SetUpTourView()
        {
            Add_AddTourEvent();
            Add_DeleteTourEvent();
            Add_DisplayTourDetails();
            loadData();

            Add_DisplayFromToWindow();
            Add_RequestTourFromServer();

            Add_UpdateTour();
            Add_LoadTourDataForSelectedItem();

            ComputedTourAttributes();
        }

        private void SetUpLogs()
        {
            loadLogData();
            Add_DisplayTourLogDetails();
            Add_AddLogEvent();
            Add_DeleteLogEvent();
        }

        private void SetUpSearch()
        {
            Add_SearchTours();
        }

        private void SetUpMenu()
        {
            Add_CreatePDFButton();
            Add_CreateSummaryButton();
            Add_DeleteAllButton();
            Add_ExportTour();
            Add_OpenWindowImportTour();
            Add_GetImportInfo();
            Add_ImportTour();
        }

        private void Add_ImportTour()
        {
            _importTourVM.confirmTourFromFolderEvent += (_, e) =>
            {
                try
                {
                    if (String.IsNullOrEmpty(_importTourVM.SelectedFolder))
                    {
                        MessageBox.Show("Please select a Tour from your Directory");
                        _loggerWrapper.Warn("User tried to import a Tour without selecting a Json File");
                    }
                    else
                    {
                        Tour tour = _report.importTour(_importTourVM.SelectedFolder);
                        if (_tourService.AddTour(tour))
                        {
                            _tour.TourData.Add(tour);
                            _loggerWrapper.Debug("User has imported a Tour from Json File");
                        }
                        else
                        {
                            MessageBox.Show("It´s not possible to import a Tour which already exist");
                            _loggerWrapper.Warn("User trys to important a Tour which already exist");
                        }
                    }
                    _importTourVM.SelectedFolder = "";
                }
                catch (Import_Exception)
                {
                    MessageBox.Show("Could not import Tour");
                    _loggerWrapper.Error("Could not import Tour");
                }
            };
        }

        private void Add_GetImportInfo()
        {
            _importTourVM.openFolderEvent += (_, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == true)
                    _importTourVM.SelectedFolder = File.ReadAllText(openFileDialog.FileName);
            };
        }

        private void Add_OpenWindowImportTour()
        {
            _menu.importTourEvent += (_, e) =>
            {
                _importView.Show();
            };
        }

        private void Add_ExportTour()
        {
            _menu.exportTourEvent += (_, e) =>
            {
                try
                {
                    if (_tour.SelectedItem != null)
                    {
                        _report.exportTour(_tour.SelectedItem);
                        MessageBox.Show("You have exported the selected Tour");
                    }
                    else
                    {
                        MessageBox.Show("Please select a Tour");
                    }
                }
                catch (Export_Exception)
                {
                    MessageBox.Show("Could not export Tour");
                    _loggerWrapper.Error("Could not export Tour");
                }
            };
        }

        public void Add_CreatePDFButton()
        {
            _menu.createPDFEvent += (_, e) =>
            {
                if (_tour.SelectedItem != null)
                {
                    _report.CreatePDFFromSelectedTour(_tour.SelectedItem);
                    MessageBox.Show("You have created a PDF with the selected Tour");
                    _loggerWrapper.Debug("User created a PDF with selected Tour");
                }
                else
                {
                    MessageBox.Show("Please select a Tour");
                }               
            };
        }

        public void Add_CreateSummaryButton()
        {
            _menu.createSummaryPDFEvent += (_, e) =>
            {
                if (_tour != null)
                {
                    _report.CreateSummary();
                    MessageBox.Show("You have created a PDF with a summary of the tours");
                    _loggerWrapper.Debug("User created a PDF with a summary of the tours");
                }
                else
                {
                    MessageBox.Show("Please create a Tour");
                }
            };
        }

        private void Add_DeleteAllButton()
        {
            _menu.deleteAllToursEvent += (_, e) =>
            {
                _tourService.DeleteAllTours();
                if (_tour.TourData.Count > 0)
                {
                    MessageBox.Show("You deleted all Tours");
                }
                _tour.TourData.Clear();
                loadData();
                _tourDetailsViewModel.TourLogData.Clear();
                loadLogData();        
            };
        }
       

        private void ComputedTourAttributes()
        {
            _tourDetailsViewModel.calculateComputedTourAttributes += (_, e) =>
            {
                if(_tour.SelectedItem != null)
                {
                    if (_tour.SelectedItem.Logs.Count > 0)
                    {
                        int numCounter = 1;
                        int counter = 0;
                        foreach (TourLog log in _tour.SelectedItem.Logs)
                        {
                            counter += log.Difficulty;
                            counter += log.TotalTime;
                            if(log.TotalTime > 0 || log.TotalTime > 0 )
                            {
                                numCounter += 2;
                            }                           
                        }
                        float temp = 0;
                        if (_tour.SelectedItem.TourDistance.Contains("."))
                        {
                            temp = float.Parse(_tour.SelectedItem.TourDistance);
                        }
                        counter += (int)temp;
                        counter /= numCounter;
                        _tour.SelectedItem.ChildFriendliness = counter;
                        _tour.SelectedItem.Popularity = _tour.SelectedItem.Logs.Count;
                    }
                    else
                    {
                        _tour.SelectedItem._childFriendliness = 0;
                        _tour.SelectedItem.Popularity = 0;
                    }
                }
            };
        }

        private void Add_LoadTourDataForSelectedItem()
        {
            _tour.loadlTourDataForSelectedItem += (_, s) =>
            {              
                if(s != null)
                {
                    _tourDetailsViewModel.TourLogData.Clear();
                    loadLogData();
                }               
            };
        }

        private void Add_UpdateTour()
        {
            _tourDetailsViewModel.updateTourEvent += (_, e) =>
            {
                if (_tour.SelectedItem != null)
                {
                    List<TourLog> selectedTourLogs = new List<TourLog>();

                    foreach (TourLog log in _tourDetailsViewModel.TourLogData)
                    {
                        selectedTourLogs.Add(log);
                    }

                    _tour.SelectedItem.Logs = selectedTourLogs;
                    _tourService.UpdateTour(_tour.SelectedItem);
                    MessageBox.Show("Tour has been updated");
                }
            };
        }

        private void Add_RequestTourFromServer()
        {
            _tourInfoViewModel.confirmTourInfo += async (_, t) =>
            {
                Tour tour = new Tour();
                try
                {
                    if (String.IsNullOrEmpty(_tourInfoViewModel.From) || String.IsNullOrEmpty(_tourInfoViewModel.To))
                    {
                        MessageBox.Show(" From and To must be filled in");
                        return;
                    }
                    else if (_tourInfoViewModel.TransportType != "fastest" &&
                    _tourInfoViewModel.TransportType != "shortest" &&
                    _tourInfoViewModel.TransportType != "pedestrian" &&
                    _tourInfoViewModel.TransportType != "bicycle" &&
                    String.IsNullOrEmpty(_tourInfoViewModel.TransportType))
                    {
                        MessageBox.Show("Transport Type doesn´t exist. " +
                            "Choose between fastest, pedestrian, shortest and bicycle");
                        return;
                    }
                    else
                    {
                        if (_tourInfoViewModel.TourTitle.Length > 40)
                        {
                            MessageBox.Show("You´re can only use 40 characters");
                        }
                        if (_tourInfoViewModel.To.Length > 70 || _tourInfoViewModel.From.Length > 70)
                        {
                            MessageBox.Show("Adress can only have a maximum of 70 characters");
                        }

                        tour = await _openMapAPI.GetTour(_tourInfoViewModel.TourTitle,
                            _tourInfoViewModel.From, _tourInfoViewModel.To, _tourInfoViewModel.TransportType);
                        if(tour != null)
                        {
                            _tourService.AddTour(tour);
                            _tour.TourData.Add(tour);
                            _loggerWrapper.Debug("User requested Tour from Server");
                        }                      
                    }
                }
                catch (OpenMapAPI_Exception)
                {
                    MessageBox.Show("Sorry, requested Tour cant be found");
                    _loggerWrapper.Warn("The requested Tour coudnt be found");
                }                
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
                    _tourDetailsViewModel.DisplayImage = _tour.SelectedItem.RouteImagePath;
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
                _loggerWrapper.Debug("User added a Tour");
            };
        }

        private void Add_DisplayFromToWindow()
        {
            _tour.displayGetTourWindow += (_, e) =>
            {
                _tourInfoView.Show();
            };
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

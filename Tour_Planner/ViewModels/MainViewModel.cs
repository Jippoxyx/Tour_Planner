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
using System.Globalization;
using System.Windows.Data;

namespace Tour_Planner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        TourService tourService;
        TourViewModel tour;
        TourDetailsViewModel tourDetailsViewModel;
        SearchBarViewModel searchVM;
        MenuViewModel menu;

        TourInfoView tourInfoView = new TourInfoView();
        TourInfoViewModel tourInfoViewModel = new TourInfoViewModel();    

        ImportTourView importView = new ImportTourView();
        ImportTourViewModel importTourVM = new ImportTourViewModel();

        HelpView helpView = new HelpView();
        HelpViewModel helpViewModel = new HelpViewModel();

        OpenMapAPI openMapAPI = new OpenMapAPI();
        ILoggerWrapper loggerWrapper = LoggerFactory.GetLogger();
        Reporting report = new Reporting();   
        Import_Export imp_exp = new Import_Export();

        public MainViewModel(MenuViewModel menu,
            TourViewModel tour,
            TourDetailsViewModel tourDetails,
            SearchBarViewModel search)
        {
            tourService = new IoCContainerConfig().tourService;
            this.tour = tour;
            this.tourDetailsViewModel = tourDetails;
            this.searchVM = search;
            this.menu = menu;

            SetUpTourView();           
            SetUpLogs();           
            SetUpSearch();           
            SetUpMenu();

            tourInfoView.DataContext = tourInfoViewModel;
            importView.DataContext = importTourVM;
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
            Add_OpenWindowHelp();
        }

        private void Add_ImportTour()
        {
            importTourVM.confirmTourFromFolderEvent += (_, e) =>
            {
                try
                {
                    if (String.IsNullOrEmpty(importTourVM.SelectedFolder))
                    {
                        MessageBox.Show("Please select a Tour from your Directory");
                        loggerWrapper.Warn("User tried to import a Tour without selecting a Json File");
                    }
                    else
                    {
                        Tour tour = imp_exp.importTour(importTourVM.SelectedFolder);
                        if (tourService.AddTour(tour))
                        {
                            this.tour.TourData.Add(tour);
                            loggerWrapper.Debug("User has imported a Tour from Json File");
                        }
                        else
                        {
                            MessageBox.Show("It´s not possible to import a Tour which already exist");
                            loggerWrapper.Warn("User trys to important a Tour which already exist");
                        }
                    }
                    importTourVM.SelectedFolder = "";
                }
                catch (Import_Exception)
                {
                    MessageBox.Show("Could not import Tour");
                    loggerWrapper.Error("Could not import Tour");
                }
            };
        }

        private void Add_GetImportInfo()
        {
            importTourVM.openFolderEvent += (_, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == true)
                    importTourVM.SelectedFolder = File.ReadAllText(openFileDialog.FileName);
            };
        }

        private void Add_OpenWindowImportTour()
        {
            menu.importTourEvent += (_, e) =>
            {
                importView.Show();
            };
        }        
        private void Add_OpenWindowHelp()
        {
            menu.helpEvent += (_, e) =>
            {
                helpView.Show();
            };
        }

        private void Add_ExportTour()
        {
            menu.exportTourEvent += (_, e) =>
            {
                try
                {
                    if (tour.SelectedItem != null)
                    {
                        imp_exp.exportTour(tour.SelectedItem);
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
                    loggerWrapper.Error("Could not export Tour");
                }
            };
        }

        public void Add_CreatePDFButton()
        {
            menu.createPDFEvent += (_, e) =>
            {
                if (tour.SelectedItem != null)
                {
                    report.CreatePDFFromSelectedTour(tour.SelectedItem);
                    MessageBox.Show("You have created a PDF with the selected Tour");
                    loggerWrapper.Debug("User created a PDF with selected Tour");
                }
                else
                {
                    MessageBox.Show("Please select a Tour");
                }               
            };
        }

        public void Add_CreateSummaryButton()
        {
            menu.createSummaryPDFEvent += (_, e) =>
            {
                if (tour != null)
                {
                    report.CreateSummary();
                    MessageBox.Show("You have created a PDF with a summary of the tours");
                    loggerWrapper.Debug("User created a PDF with a summary of the tours");
                }
                else
                {
                    MessageBox.Show("Please create a Tour");
                }
            };
        }

        private void Add_DeleteAllButton()
        {
            menu.deleteAllToursEvent += (_, e) =>
            {
                tourService.DeleteAllTours();
                if (tour.TourData.Count > 0)
                {
                    MessageBox.Show("You deleted all Tours");
                }
                tour.TourData.Clear();
                loadData();
                tourDetailsViewModel.TourLogData.Clear();
                loadLogData();        
            };
        }
       

        private void ComputedTourAttributes()
        {
            tourDetailsViewModel.calculateComputedTourAttributes += (_, e) =>
            {
                if(tour.SelectedItem != null)
                {
                    if (tour.SelectedItem.Logs.Count > 0)
                    {
                        int numCounter = 1;
                        int counter = 0;
                        foreach (TourLog log in tour.SelectedItem.Logs)
                        {
                            counter += log.Difficulty;
                            counter += log.TotalTime;
                            if(log.TotalTime > 0 || log.TotalTime > 0 )
                            {
                                numCounter += 2;
                            }                           
                        }
                        float temp = 0;
                        if (tour.SelectedItem.TourDistance.Contains(".") || tour.SelectedItem.TourDistance.Contains(","))
                        {
                            tour.SelectedItem.TourDistance.Replace(",", ".");
                            temp = float.Parse(tour.SelectedItem.TourDistance, CultureInfo.InvariantCulture);
                        }
                        counter += (int)temp;
                        counter /= numCounter;
                        tour.SelectedItem.ChildFriendliness = counter;
                        tour.SelectedItem.Popularity = tour.SelectedItem.Logs.Count;
                    }
                    else
                    {
                        tour.SelectedItem._childFriendliness = 0;
                        tour.SelectedItem.Popularity = 0;
                    }
                }
            };
        }

        private void Add_LoadTourDataForSelectedItem()
        {
            tour.loadlTourDataForSelectedItem += (_, s) =>
            {              
                if(s != null)
                {
                    tourDetailsViewModel.TourLogData.Clear();
                    loadLogData();
                }               
            };
        }

        private void Add_UpdateTour()
        {
            tourDetailsViewModel.updateTourEvent += (_, e) =>
            {
                if (tour.SelectedItem != null)
                {
                    List<TourLog> selectedTourLogs = new List<TourLog>();

                    foreach (TourLog log in tourDetailsViewModel.TourLogData)
                    {
                        selectedTourLogs.Add(log);
                    }

                    tour.SelectedItem.Logs = selectedTourLogs;
                    tourService.UpdateTour(tour.SelectedItem);
                    MessageBox.Show("Tour has been updated");
                }
            };
        }

        private void Add_RequestTourFromServer()
        { 
            tourInfoViewModel.confirmTourInfo += async (_, t) =>
            {
                Tour tour = new Tour();
                try
                {
                    if (String.IsNullOrEmpty(tourInfoViewModel.From) || String.IsNullOrEmpty(tourInfoViewModel.To))
                    {
                        MessageBox.Show(" From and To must be filled in");
                        return;
                    }
                    else if (tourInfoViewModel.TransportType != "fastest" &&
                    tourInfoViewModel.TransportType != "shortest" &&
                    tourInfoViewModel.TransportType != "pedestrian" &&
                    tourInfoViewModel.TransportType != "bicycle" &&
                    String.IsNullOrEmpty(tourInfoViewModel.TransportType))
                    {
                        MessageBox.Show("Transport Type doesn´t exist. " +
                            "Choose between fastest, pedestrian, shortest and bicycle");
                        return;
                    }
                    else
                    {
                        if (tourInfoViewModel.TourTitle.Length > 40)
                        {
                            MessageBox.Show("You´re can only use 40 characters");
                        }
                        if (tourInfoViewModel.To.Length > 70 || tourInfoViewModel.From.Length > 70)
                        {
                            MessageBox.Show("Adress can only have a maximum of 70 characters");
                        }

                        tour = await openMapAPI.GetTour(tourInfoViewModel.TourTitle,
                            tourInfoViewModel.From, tourInfoViewModel.To, tourInfoViewModel.TransportType);
                        if(tour != null)
                        {
                            tourService.AddTour(tour);
                            this.tour.TourData.Add(tour);
                            loggerWrapper.Debug("User requested Tour from Server");
                        }                      
                    }
                }
                catch (OpenMapAPI_Exception)
                {
                    MessageBox.Show("Sorry, requested Tour cant be found");
                    loggerWrapper.Warn("The requested Tour coudnt be found");
                }                
            };
        }

        private void loadLogData()
        {
            if (tour.SelectedItem != null)
            {
                foreach (var log in tourService.GetLogData(tour.SelectedItem))
                {
                    tourDetailsViewModel.TourLogData.Add(log);
                }
            }
        }

        private void loadData()
        {
            foreach (var tour in tourService.GetData())
            {
                this.tour.TourData.Add(tour);
            }
        }

      

        void Add_SearchTours()
        {
            searchVM.SearchBoxChanged += (_, searchTours) =>
            {
                int result;
                List<Tour> tourSearch = new List<Tour>();
                List<Tour> resultTour = new List<Tour>();
                tourSearch = tourService.GetData();
                searchVM.cmbTour.Clear();
                if (searchTours != null)
                {
                    foreach (Tour t in tourSearch)
                    {
                        if (t.Title.Contains(searchTours, StringComparison.OrdinalIgnoreCase) || t.From.Contains(searchTours, StringComparison.OrdinalIgnoreCase) 
                        || t.To.Contains(searchTours, StringComparison.OrdinalIgnoreCase) || t.Desciption.Contains(searchTours, StringComparison.OrdinalIgnoreCase))
                        {
                            resultTour.Add(t);
                            //_searchVM.cmbTour.Items.Add(t.Title);
                            searchVM.cmbTour.Add(t);

                        }
                        else
                        {
                            foreach (TourLog log in t.Logs)
                            {
                                if (t.Logs == null)
                                {
                                    break;
                                }
                                if ((log.Comment != null && log.Comment.Contains(searchTours, StringComparison.OrdinalIgnoreCase)) 
                                || (log.Date != null && log.Date.Contains(searchTours, StringComparison.OrdinalIgnoreCase)))
                                {
                                    resultTour.Add(t);
                                    //_searchVM.cmbTour.Items.Add(t.Title);
                                    //_searchVM.cmbTour.Clear();
                                    searchVM.cmbTour.Add(t);

                                }
                            }
                        }
                    }
                    result = resultTour.Count;
                    //_tour.SelectedItem = _searchVM.cmbSelTour;
                    //_tourDetailsViewModel.Tour = _searchVM.cmbSelTour;
                }
                else
                { result = 0;}

                this.searchVM.DisplayResult($"Found {result} Tour(s)");
            };
        }

        private void Add_DisplayTourDetails()
        {
            tour.displayTourDetails += (_, t) =>
            {
                tourDetailsViewModel.Tour = tour.SelectedItem;
                
                if (tour.SelectedItem != null)
                {
                    tourDetailsViewModel.TourLogData
                = new ObservableCollection<TourLog>(tour.SelectedItem.Logs);
                    tourDetailsViewModel.DisplayImage = tour.SelectedItem.RouteImagePath;
                }                
            };
        }

        private void Add_DeleteTourEvent()
        {
            tour.deleteTourEvent += (_, t) =>
            {
                tourService.DeleteSelectedTour(t);
                tour.TourData.Clear();
                loadData();
                tourDetailsViewModel.TourLogData.Clear();
                loadLogData();
            };
        }

        private void Add_AddTourEvent()
        {
            tour.addTourEvent += (_, t) =>
            {
                tourService.AddTour(t);
                tour.TourData.Add(t);
                loggerWrapper.Debug("User added a Tour");
            };
        }

        private void Add_DisplayFromToWindow()
        {
            tour.displayGetTourWindow += (_, e) =>
            {
                tourInfoView.Show();
            };
        }

        private void Add_DisplayTourLogDetails()
        {
            tourDetailsViewModel.displayTourLogDetails += (_, t) =>
            {
                if (tour.SelectedItem != null && tourDetailsViewModel.SelectedLog != null)
                {
                    tourDetailsViewModel.TourLog = tourDetailsViewModel.SelectedLog;
                }
            };
        }

        private void Add_AddLogEvent()
        {
            tourDetailsViewModel.addLogEvent += (_, l) =>
            {
                if (tour.SelectedItem != null)
                {
                    tourService.AddLog(tour.SelectedItem, l);
                    tourDetailsViewModel.TourLogData.Add(l);
                }
            };
        }

        private void Add_DeleteLogEvent()
        {
            tourDetailsViewModel.deleteLogEvent += (_, l) =>
            {
                if (tourDetailsViewModel.SelectedLog != null)
                {
                    tourService.DeleteSelectedTourLog(tour.SelectedItem, l);
                    tourDetailsViewModel.TourLogData.Clear();
                    loadLogData();
                }
            };
        }
    }
}

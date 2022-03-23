﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//using Tour_Planner.Models;
using Tour_Planner.Model;
using Tour_Planner.ViewModels.Utility;

namespace Tour_Planner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        TourViewModel tour;

        public MainViewModel(MenuViewModel menu, 
            TourViewModel tour, 
            TourDetailsViewModel tourDetails, 
            SearchBarViewModel search)
        {
            this.tour = tour;
            SetUpTourView();
        }

        private void SetUpTourView()
        {
            Add_AddTourEvent();
            //Add_DeleteTourEvent();
            Add_DeleteAllEvent();           
        }

        /*
        private void Add_DeleteTourEvent()
        {
            tour.deleteTourEvent += (_, t) =>
            {
                DeleteTourExecute(t);
            };
        }
        */

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

        /*
        private void DeleteTourExecute(Tour t)
        {
            List<Tour> items = tour.TourData.Where(x => x.Id == t.Id).ToList();
            foreach(Tour tou in items)
            {
                tour.TourData.Remove(tou);
            }
        }
        */

        private void AddTourExecute(Tour t)
        {                   
            tour.TourData.Add(t);
        }
    }
}

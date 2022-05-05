﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Models;

namespace Tour_Planner.DAL
{
    public interface ITourManager
    {

        List<Tour> GetTourData();
        List<TourLog> GetTourLogData(Tour tour);
        Tour CreateTour();
        void DeleteAllTours();
        void DeleteTour(Tour tour);
        TourLog CreateLog(Tour tour);
        void DeleteTourLog(Tour tour, TourLog log);


    }
}

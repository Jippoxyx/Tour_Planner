using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tour_Planner.DAL;
using Tour_Planner.Logging;
using Tour_Planner.Models;

namespace Tour_Planner.BL.Service
{
    public class TourService 
    {
        private readonly ITourManager _tourManager;
        ILoggerWrapper _loggerWrapper = LoggerFactory.GetLogger();

        public TourService(ITourManager tourManager)
        {
            _tourManager = tourManager;        
        }

        public List<Tour> GetData()
        {
            return _tourManager.GetTourData();
        }

        public bool AddTour(Tour tour)
        {
            return (_tourManager.CreateTour(tour));           
        }

        public void DeleteSelectedTour(Tour tour)
        {
            _tourManager.DeleteTour(tour);
        }
        public void DeleteSelectedTourLog(Tour tour,TourLog log)
        {
            _tourManager.DeleteTourLog(tour, log);
        }

        public List<TourLog> GetLogData(Tour tour)
        {
            return _tourManager.GetTourLogData(tour);
        }

        public void AddLog(Tour tour, TourLog log)
        {
            _tourManager.CreateLog(tour, log);
        }       

        public void DeleteAllTours()
        {
            _tourManager.DeleteAllTours();
        }
    }
}
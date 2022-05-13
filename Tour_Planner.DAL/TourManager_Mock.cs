using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Models;

namespace Tour_Planner.DAL
{
    public class TourManager_Mock : ITourManager
    {

        List<Tour> _tourData = new List<Tour>();

        public TourManager_Mock()
        {
            CreateTours();
        }

        private void CreateTours()
        {        
            _tourData.Add(new Tour()
            {
                Title = "0_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-70967728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "0 Log",
                    Id = new(" 0f8fad5b-d9cb-469f-a165-70967728950e") } },
            });
            _tourData.Add(new Tour()
            {
                Title = "1_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-70667728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "1 Log",
                    Id = new(" 0f1fad5b-d9cb-469f-a165-70967728950e") } }
            });
            _tourData.Add(new Tour()
            {
                Title = "2_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-70867728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "2 Log",
                    Id = new(" 0f3fad5b-d9cb-469f-a165-70967728950e") } }
            });
            _tourData.Add(new Tour()
            {
                Title = "3_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-70767728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "3 Log",
                    Id = new(" 0f4fad5b-d9cb-469f-a165-70967728950e") } }
            });
            _tourData.Add(new Tour()
            {
                Title = "4_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-70567728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "4 Log",
                    Id = new(" 0f5fad5b-d9cb-469f-a165-70967728950e") } }
            });
            _tourData.Add(new Tour()
            {
                Title = "5_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-70467728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "5 Log" ,
                    Id = new(" 0f6fad5b-d9cb-469f-a165-70967728950e")} }
            });
            _tourData.Add(new Tour()
            {
                Title = "6_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-70367728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "6 Log",
                    Id = new(" 0f7fad5b-d9cb-469f-a165-70967728950e")} }
            });
            _tourData.Add(new Tour()
            {
                Title = "7_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-72867728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "7 Log",
                    Id = new(" 0f9fad5b-d9cb-469f-a165-70967728950e") } }
            });
            _tourData.Add(new Tour()
            {
                Title = "8_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-70167728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "8 Log",
                    Id = new(" 0f8fad4b-d9cb-469f-a165-70967728950e") } }
            });
        }

        public List<Tour> GetTourData()
        {
            return _tourData;
        }

        public void CreateTour(Tour tour)
        {
            _tourData.Add(tour);
        }

        public void DeleteTour(Tour tour)
        {
            if(tour != null)
            {
                List<Tour> items = _tourData.Where(x => x.Id == tour.Id).ToList();
                foreach (Tour tou in items)
                {
                    _tourData.Remove(tou);
                }
            }
        }
        
        public void CreateLog(Tour tour, TourLog log)
        {        
            tour.Logs.Add(log);
        }

        public void DeleteTourLog(Tour tour, TourLog log)
        {
            try
            {
                foreach (Tour tou in _tourData)
                {
                    if (tour.Id == tou.Id)
                    {
                        foreach (TourLog losss in tou.Logs)
                        {
                            if (log.Id == losss.Id)
                            {
                                tou.Logs.Remove(losss);
                            }
                        }
                    }
                }
            }
            catch (InvalidOperationException) 
            {
                return;
            }                  
        }

        public List<TourLog> GetTourLogData(Tour tour)
        {
            List<TourLog> logList = new List<TourLog>();
            if(tour != null)
            {
                foreach (TourLog log in tour.Logs)
                {
                    logList.Add(log);
                }
               
            }
            return logList;
        }

        public void DeleteAllTours()
        {
            _tourData.Clear();
        }
    }
}

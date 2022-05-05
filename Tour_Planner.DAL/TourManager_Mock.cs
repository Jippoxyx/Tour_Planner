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
        List<TourLog> _logData = new List<TourLog>();

        public TourManager_Mock()
        {
            CreateTours();
        }

        private void CreateTours()
        {
            Tour first_Tour = new Tour()
            {
                Title = "0_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-70967728950e")
            };
            first_Tour.Logs = new List<TourLog>() { new TourLog() { Comment = "0 Log" } };          

            _tourData.Add(new Tour()
            {
                Title = "0_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-70967728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "0 Log" } },
            });
            _tourData.Add(new Tour()
            {
                Title = "1_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-70667728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "1 Log" } }
            });
            _tourData.Add(new Tour()
            {
                Title = "2_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-70867728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "2 Log" } }
            });
            _tourData.Add(new Tour()
            {
                Title = "3_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-70767728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "3 Log" } }
            });
            _tourData.Add(new Tour()
            {
                Title = "4_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-70567728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "4 Log" } }
            });
            _tourData.Add(new Tour()
            {
                Title = "5_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-70467728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "5 Log" } }
            });
            _tourData.Add(new Tour()
            {
                Title = "6_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-70367728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "6 Log" } }
            });
            _tourData.Add(new Tour()
            {
                Title = "7_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-72867728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "7 Log" } }
            });
            _tourData.Add(new Tour()
            {
                Title = "8_Tour",
                Id = new(" 0f8fad5b-d9cb-469f-a165-70167728950e"),
                Logs = new List<TourLog>() { new TourLog() { Comment = "8 Log" } }
            });
        }

        public List<Tour> GetTourData()
        {
            return _tourData;
        }

        public Tour CreateTour()
        {
            Tour tou = new Tour() { Title = "new_Tour", Id = Guid.NewGuid() };
            _tourData.Add(tou);
            return tou;
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
        
        public TourLog CreateLog()
        {
            TourLog log = new TourLog() { Comment = "new_Tour", Id = Guid.NewGuid() };
            _logData.Add(log);
            return log;

        }

        public void DeleteTourLog(TourLog log)
        {
            if(log != null)
            {
                List<TourLog> items = _logData.Where(x => x.Id == log.Id).ToList();
                foreach (TourLog lo in items)
                {
                    _logData.Remove(lo);
                }
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

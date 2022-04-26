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
            _tourData.Add(new Tour() { Title = "0_Tour" });
            _tourData.Add(new Tour() { Title = "1_Tour" });
            _tourData.Add(new Tour() { Title = "2_Tour" });
            _tourData.Add(new Tour() { Title = "3_Tour" });
            _tourData.Add(new Tour() { Title = "4_Tour" });
            _tourData.Add(new Tour() { Title = "5_Tour" });
            _tourData.Add(new Tour() { Title = "6_Tour" });
            _tourData.Add(new Tour() { Title = "7_Tour" });
            _tourData.Add(new Tour() { Title = "8_Tour" });
        }

        public void AddTour(Tour tour)
        {
            _tourData.Add(tour);
        }

        public void DeleteTour(Tour tour)
        {
            throw new NotImplementedException();
        }

        public void UpdateTours(Tour tour)
        {
            throw new NotImplementedException();
        }
    }
}

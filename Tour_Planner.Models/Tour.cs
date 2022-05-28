using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.Models
{
    public class Tour
    {
        public Guid Id { get; set; }

        public string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (value.Length < 148 && value.Length > 0)
                {
                    _title = value;
                }
                else
                {
                    _title = "";
                }
            }
        }
        public string _description;
        public string Desciption
        {
            get { return _description; }
            set
            {
                if (value.Length < 148 && value.Length > 0)
                {
                    _description = value;
                }
                else
                {
                    _description = "";
                }
            }
        }
        //vaidated in MainViewModel
        public string From { get; set; }
        //vaidated in MainViewModel
        public string To { get; set; }
        //vaidated in MainViewModel
        public string TransportType { get; set; }


        public string _tourDistance;
        public string TourDistance
        {
            get { return _tourDistance; }
            set
            {
                if (value.Length < 27 && value.Length > 0)
                {
                    _tourDistance = value;
                }
                else
                {
                    _tourDistance = "";
                }
            }
        }

        public string _estimatedTime;
        public string EstimatedTime
        {
            get { return _estimatedTime; }
            set
            {
                if (value.Length < 27 && value.Length > 0)
                {
                    _estimatedTime = value;
                }
                else
                {
                    _estimatedTime = "";
                }
            }
        }
        public string RouteImagePath { get; set; }
        public string Session { get; set; }
        public string BoundingBox { get; set; }

        public List<TourLog> Logs { get; set; } = new List<TourLog>();


        public int _childFriendliness;
        public int ChildFriendliness
        {
            get { return _childFriendliness; }
            set
            {
                _childFriendliness = value;               
            }
        }
        public int _popularity;
        public int Popularity
        {
            get { return _popularity; }
            set
            {
                _popularity = Logs.Count;
            }
        }
    }
}

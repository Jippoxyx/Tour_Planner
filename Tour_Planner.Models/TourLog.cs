using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tour_Planner.Models
{
    public class TourLog
    {
        public Guid Id { get; set; }

        public string _date;
        public string Date
        {
            get { return _date; }

            set
            {
                if (DateTime.TryParse(value, out _))
                {
                    _date = value;
                }
            }
        }

        public int _time;
        public int Time
        {
            get { return _time; }
            set
            {
                if (value < 10000000)
                {
                    _time = value;
                }
                else
                {
                    _time = 0;
                }
            }
        }

        public string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (value?.Length < 148 && value?.Length > 0)
                {
                    _comment = value;
                }
                else
                {
                    _comment = "";
                }

            }
        }
        public int _difficulty;
        public int Difficulty
        {
            get { return _difficulty; }
            set
            {
                if (value < 11 && value > 0)
                {
                    _difficulty = value;
                }
                else
                {
                    _difficulty = 0;
                }
            }
        }

        public int _totalTime;
        public int TotalTime
        {
            get => _totalTime;
            set
            {
                if (value < 200000 && value > 0)
                {
                    _totalTime = value;
                }
                else
                {
                    _totalTime = 0;
                }
            }
        }


        public int _rating;
        public int Rating
        {
            get => _rating;
            set
            {
                if (value < 11 && value > 0)
                {
                    _rating = value;
                }
                else
                {
                    _rating = 0;
                }
            }
        }
    }
}

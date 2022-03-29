using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Model;
using Tour_Planner.ViewModels.Utility;

namespace Tour_Planner.ViewModels
{
    public class TourDetailsViewModel : ViewModelBase
    {
       public Tour _tourDetail;

        public Tour Tour
        {
            get { return _tourDetail; }
            set
            {
                _tourDetail = value;
                OnPropertyChanged();
            }
        }


    }
}

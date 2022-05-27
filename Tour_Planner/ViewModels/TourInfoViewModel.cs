using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tour_Planner.ViewModels.Utility;

namespace Tour_Planner.ViewModels
{
    class TourInfoViewModel : ViewModelBase
    {
        public ICommand ConfirmTourInfoCommand { get; set; }
        
        public event EventHandler confirmTourInfo;
        
        public TourInfoViewModel()
        {
            ConfirmTourInfoCommand = new RelayCommand((_) =>
            {
                this.confirmTourInfo?.Invoke(null, EventArgs.Empty);
            });           
        }

        string _from;
        public string From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged();
            }
        }

        string _to;
        public string To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged();
            }
        }

        public string _transportType;
        public string TransportType 
        {
            get { return _transportType; }
            set
            {
                _transportType = value;
                OnPropertyChanged();
            }
        }

        string _tourTitle;
        public string TourTitle
        { 
            get { return _tourTitle; } 
            set { _tourTitle = value; 
                OnPropertyChanged(); 
            } 
        }
    }
}

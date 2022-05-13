using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tour_Planner.Models;
using Tour_Planner.ViewModels.Utility;

namespace Tour_Planner.ViewModels
{
    public class TourDetailsViewModel : ViewModelBase
    {
        public ICommand AddLogCommand { get; set; }
        public ICommand DeleteLogCommand { get; set; }
        public event EventHandler<TourLog> addLogEvent;
        public event EventHandler<TourLog> deleteLogEvent;
        public event EventHandler<TourLog> displayTourLogDetails;

        public TourViewModel _tourViewModel;

        public TourDetailsViewModel()
        {
            AddLogCommand = new RelayCommand((_) =>
            {
                TourLog l = new TourLog();
                this.addLogEvent?.Invoke(this, l);
            });
            DeleteLogCommand = new RelayCommand((_) =>
            {
                TourLog l = new TourLog();
                this.deleteLogEvent?.Invoke(this, SelectedLog);
            });
        }

        public ObservableCollection<TourLog> _tourLogData { get; set; }
            = new ObservableCollection<TourLog>();

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

        public TourLog _tourLogDetail;

        public TourLog TourLog
        {
            get { return _tourLogDetail; }
            set
            {
                _tourLogDetail = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TourLog> TourLogData
        {
            get { return _tourLogData; }
            set
            {
                _tourLogData = value;
                OnPropertyChanged();
            }
        }

        private TourLog _selectedLogItem;
        public TourLog SelectedLog
        {
            get { return _selectedLogItem; }
            set
            {
                _selectedLogItem = value;
                OnPropertyChanged();
                displayTourLogDetails?.Invoke(this, _selectedLogItem);
            }
        }

        private static string GetImageSourceFromConfig()
        {
            var file = File.ReadAllText($"..\\..\\..\\config.json");
            JObject text = JsonConvert.DeserializeObject<JObject>(file);
            string source = text["imageSource"].ToString();
            return source;
        }
        public string DisplayImage
        {
            get { return GetImageSourceFromConfig();}
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Tour_Planner.Model;
using Tour_Planner.ViewModels.Utility;

namespace Tour_Planner.ViewModels
{
    class TourViewModel : ViewModelBase
    {
        public ICommand AddTourCommand { get; set; }
        public ICommand DeleteAllToursCommand { get; set; }

        public ObservableCollection<Tour> TourData { get; }
        = new ObservableCollection<Tour>();

        private Tour _tour;
        public Tour Tour
        {
            get { return _tour; }
            set
            {
                _tour = value;
                OnPropertyChanged();
            }
        }

        public TourViewModel()
        {
            AddTourCommand = new RelayCommand(x => AddTourExecute(), x => true);
            DeleteAllToursCommand = new RelayCommand(x => DeleteTourExecute(), x => true);
        }

        private void DeleteTourExecute()
        {
            TourData.Clear();
        }

        private void AddTourExecute()
        {
            Tour t = new Tour();
            t.Id = Guid.NewGuid();
            t.Title = "NewTour";
            t.Desciption = "pretty cool!";
            TourData.Add(t);
        }
    }
}

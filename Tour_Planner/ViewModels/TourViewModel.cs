using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Tour_Planner.BL;
using Tour_Planner.Models;
using Tour_Planner.ViewModels.Utility;

namespace Tour_Planner.ViewModels
{
    public class TourViewModel : ViewModelBase
    {
        public ObservableCollection<Tour> TourData { get; set; }
            = new ObservableCollection<Tour>();
        public ICommand AddTourCommand { get; set; }
        public ICommand DeleteTourCommand { get; set; }

        public event EventHandler<Tour> addTourEvent;
        public event EventHandler<Tour> deleteTourEvent;
        public event EventHandler<Tour> displayTourDetails;

        private Tour _tour = new Tour();
        public Tour Tour
        {
            get { return _tour; }
            set
            {
                _tour = value;
                OnPropertyChanged();
            }
        }

        private  Tour _selectedItem;
        public Tour SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                displayTourDetails?.Invoke(this, _selectedItem);
            }
        }

        public TourViewModel()
        {
            AddTourCommand = new RelayCommand((_) =>
            {
                Tour t = new Tour();
                this.addTourEvent?.Invoke(this, t);
            }
                );

            DeleteTourCommand = new RelayCommand((_) =>
            {
                this.deleteTourEvent?.Invoke(this, SelectedItem);
            });               
        }  
    }
}

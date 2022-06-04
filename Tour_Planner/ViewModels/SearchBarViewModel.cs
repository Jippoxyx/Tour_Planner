using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tour_Planner.ViewModels.Utility;
using Tour_Planner.BL;
using Tour_Planner.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Tour_Planner.ViewModels
{
    public class SearchBarViewModel : ViewModelBase
    {

        public event EventHandler<string> SearchBoxChanged;
        public event EventHandler<Tour> displaySearchResult;
        public event EventHandler<Tour> loadTourDataForSelectedItem;

        public ObservableCollection<Tour> cmbTour { get; set; }
            = new ObservableCollection<Tour>();
        public ICommand SearchBtn { get; }

        public string searchText;
        public string resultText = "";

        //public ComboBox cmbTour { get; set; }
         //   = new ComboBox();

        public SearchBarViewModel()
        {
            SearchBtn = new RelayCommand((_) =>
            {
                SearchBoxChanged?.Invoke(this, SearchBox);
            });
        }

        public string SearchBox
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged();
            }
        }

        public void SearchTour(string search)
        {
           // tourFactory.SearchTour(search);
        }

        public string ResultBox
        {
            get { return resultText; }
            set
            {
                resultText = value;
                OnPropertyChanged();
            }
        }

        /* public List<Tour> _cmbTour;
         public Tour _cmbSelTour;

         public List<Tour> cmbTour
         {
             get { return _cmbTour; }
         }

         public Tour cmbSelTour
         {
             get { return _cmbSelTour; }
             set
             {
                 _cmbSelTour = value;
                 OnPropertyChanged();
                 displayTourDetails?.Invoke(this, _cmbSelTour);
                 loadlTourDataForSelectedItem?.Invoke(this, _cmbSelTour);
             }
         }*/

        private Tour _cmbSelTour;
        public Tour cmbSelTour
        {
            get { return _cmbSelTour; }
            set
            {
                _cmbSelTour = value;
                OnPropertyChanged();
                displaySearchResult?.Invoke(this, _cmbSelTour);
                loadTourDataForSelectedItem?.Invoke(this, _cmbSelTour);
            }
        }
        public void DisplayResult(string resultText)
        {
            //displaySearchResult?.Invoke(this, _cmbSelTour);

            ResultBox = resultText;
        }
    }
}

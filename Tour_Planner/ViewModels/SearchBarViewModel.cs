using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tour_Planner.ViewModels.Utility;
using Tour_Planner.BL;

namespace Tour_Planner.ViewModels
{
    /*
         * DummySearch v1
         * TODO:
         * SearchTour returntyp ändern
         * 
         * Wenn TourDaten in DB gespeichert werden können
         * -> DB verbinden und nach Namen suchen
         * 
         * Frage: label klickbar und somit werden erst daten angezeigt?
         */
    public class SearchBarViewModel : ViewModelBase
    {
        //private readonly ITourFactory tourFactory = new TourFactory();

        public event EventHandler<string> SearchBoxChanged;
        public ICommand SearchBtn { get; }

        public string searchText;
        public string resultText = "leer";

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

        public void DisplayResult(string resultText)
        {
            ResultBox = resultText;
        }
    }
}

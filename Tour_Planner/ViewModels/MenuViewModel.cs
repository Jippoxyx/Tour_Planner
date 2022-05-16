using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tour_Planner.Models;
using Tour_Planner.ViewModels.Utility;

namespace Tour_Planner.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public ICommand DeleteAllToursCommand { get; set; }
        public ICommand CreatePDFCommand { get; set; }
        public event EventHandler deleteAllToursEvent;
        public event EventHandler createPDFEvent;

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

        public MenuViewModel()
        {
            DeleteAllToursCommand = new RelayCommand((_) =>
            {
                this.deleteAllToursEvent?.Invoke(null, EventArgs.Empty);
            });

            CreatePDFCommand = new RelayCommand((_) =>
            {
                this.createPDFEvent?.Invoke(null, EventArgs.Empty);
            });
        }
    }
}

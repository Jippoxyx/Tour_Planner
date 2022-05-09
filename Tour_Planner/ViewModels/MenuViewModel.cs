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
        public event EventHandler deleteAllToursEvent;

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
        }
    }
}

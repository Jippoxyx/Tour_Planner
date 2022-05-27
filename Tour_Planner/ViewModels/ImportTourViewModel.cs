using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tour_Planner.Models;
using Tour_Planner.ViewModels.Utility;


namespace Tour_Planner.PL.ViewModels
{
    public class ImportTourViewModel : ViewModelBase
    {
        public ICommand OpenFolderCommand { get; set; }
        public ICommand ConfirmTourFromFolderCommand { get; set; }

        public event EventHandler openFolderEvent;
        public event EventHandler confirmTourFromFolderEvent;

        public ImportTourViewModel()
        {
            OpenFolderCommand = new RelayCommand((_) =>
            {
                this.openFolderEvent?.Invoke(this, EventArgs.Empty);
            });

            ConfirmTourFromFolderCommand = new RelayCommand((_) =>
            {
                this.confirmTourFromFolderEvent?.Invoke(this, EventArgs.Empty);
            });
        }

        public string _selectedFolder;
        public string SelectedFolder 
        {
            get { return _selectedFolder; }
            set 
            { 
                _selectedFolder = value;
                OnPropertyChanged();
            } 
        }

    }
}
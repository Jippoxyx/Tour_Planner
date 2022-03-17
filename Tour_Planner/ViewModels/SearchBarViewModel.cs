using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tour_Planner
{
    public class SearchBarViewModel : BaseViewModel
    {
        public event EventHandler<string> SearchTextChanged;

        public ICommand SearchCommand { get; }

        private string searchText;

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged();
            }
        }

        public SearchBarViewModel()
        {
            SearchCommand = new SearchCommand((_) =>
            {
                this.SearchTextChanged?.Invoke(this, SearchText);
            });
        }

    }
}

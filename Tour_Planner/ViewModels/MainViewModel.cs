using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.ViewModels

{
    public class MainViewModels : BaseViewModel
    {
        public readonly ResultViewModel resultView;
        public readonly ISearchEngine searchEngine;

        public MainViewModels(SearchBarViewModel searchBar, ResultViewModel resultV, ISearchEngine searchEngine)
        {
            searchBar.SearchTextChanged += (_, searchText) =>
            {
                SearchTour(searchText);
            };
            this.resultView = resultV;
            this.searchEngine = searchEngine;
        }

        public void SearchTour(string searchText)
        {
            var result = String.Join("\n", this.searchEngine.SearchFor(searchText));
            this.resultView.DisplayResult(result);
        }
    }
}

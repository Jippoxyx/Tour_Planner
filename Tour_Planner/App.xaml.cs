using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Tour_Planner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var searchBarViewModel = new SearchBarViewModel();
            var searchEngine = new SearchEngine();
            var resultViewModel = new ResultViewModel();
            var menu = new MenuView();
            var tour = new TourView();
            var log = new LogView();
            

            var window = new MainWindow
            {
                DataContext = new MainViewModel(searchBarViewModel, resultViewModel, searchEngine),
                Menu = { DataContext = menu },
                SearchBar = { DataContext = searchBarViewModel },
                ResultView = { DataContext = resultViewModel },
                Tour = {DataContext = tour},
                Log = {DataContext = log }
            };
            window.Show();
        }

    }
}

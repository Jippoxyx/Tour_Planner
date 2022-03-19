using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tour_Planner.ViewModels;


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
            var tourDetails = new TourDetailsView();
            

            var window = new MainWindow
            {
                DataContext = new MainViewModels(searchBarViewModel, resultViewModel, searchEngine),
                Menu = { DataContext = menu },
                SearchBar = { DataContext = searchBarViewModel },
                ResultView = { DataContext = resultViewModel },
                Tour = {DataContext = tour},
                TourDetails = {DataContext = tourDetails}
            };
            window.Show();
        }

    }
}

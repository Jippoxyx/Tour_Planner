using System.Windows;
using Tour_Planner.PL;                  // löschen nach verschiebung des DB aufrufs
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
            //
            /*var searchEngine = new SearchEngine();
            var resultViewModel = new ResultViewModel();
            */
            var menu = new MenuViewModel();
            var tour = new TourViewModel();
            var tourDetails = new TourDetailsViewModel();
            var searchBar = new SearchBarViewModel();

            //-------------DB-Dummy-Connection------------------
            var db = new ConfigAccess();                        // verschieben zu den Buttons
            //-------------DB-Dummy-Connection------------------

            var window = new MainWindow()
            {
               DataContext = new MainViewModel(menu, tour, tourDetails, searchBar),
               Menu = { DataContext = menu },
               SearchBar = { DataContext = searchBar},
               Tour = {DataContext = tour},
               TourDetails = {DataContext = tourDetails}
            };
            window.Show();
        }
    }
}

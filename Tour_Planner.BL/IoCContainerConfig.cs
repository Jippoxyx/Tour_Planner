using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tour_Planner.BL.Service;
using Tour_Planner.DAL;

namespace Tour_Planner.Bl
{
    public class IoCContainerConfig
    {
        private readonly ServiceProvider _serviceProvider;

        /// <summary>
        /// Builds the IoC service provider, see also App.xaml which instantiates it as a resource
        /// </summary>
        public IoCContainerConfig()
        {
            var services = new ServiceCollection();

            // whenever an IArgumentHandler is required, the service will inject a CommandLineArgumentHandler
            // it will always provide the same CommandLineArgumentHandler instance, because we register it as a singleton
            services.AddSingleton<ITourManager, TourManager_Mock>();

          

            // register, the ServiceProvider will provide the constructor parameters
            // based on the configuration above
            services.AddSingleton<TourService>();

            // finish configuration and build the provider
            _serviceProvider = services.BuildServiceProvider();          
        }
        /// <summary>
        /// Getter for retrieving and binding the MainViewModel in MainWindow.xaml as its DataContext
        /// </summary>
        public TourService tourService
            => _serviceProvider.GetService<TourService>(); 
    }
}


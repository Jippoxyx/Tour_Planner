using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.Models
{
    public class Tours : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string tours;

    }
}

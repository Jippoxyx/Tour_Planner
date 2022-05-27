using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.BL.Exceptions
{
    public  class Export_Exception : Exception
    {
        public Export_Exception()
        {
        }

        public Export_Exception(string message)
            : base(message)
        {
        }

        public Export_Exception(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

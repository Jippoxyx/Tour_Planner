using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.BL.Exceptions
{
    public  class OpenMapAPI_Exception : Exception
    {
        public OpenMapAPI_Exception()
        {
        }

        public OpenMapAPI_Exception(string message)
            : base(message)
        {
        }

        public OpenMapAPI_Exception(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

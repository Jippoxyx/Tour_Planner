using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.BL.Exceptions
{
    public  class Import_Exception : Exception
    {
        public Import_Exception()
        {
        }

        public Import_Exception(string message)
            : base(message)
        {
        }

        public Import_Exception(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

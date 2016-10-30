using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException() : base("duplicate id: {0}")
        {
            //todokd
        }
    }
}

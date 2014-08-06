using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Exceptions
{
    public class CarNotRentedException : ApplicationException
    {
        public CarNotRentedException(string message)
            : base(message) {
        }

        public CarNotRentedException(string message, Exception exception)
            : base(message, exception) {
        }
    }
}

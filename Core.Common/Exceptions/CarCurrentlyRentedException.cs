using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Exceptions
{
    public class CarCurrentlyRentedException : ApplicationException
    {
        public CarCurrentlyRentedException(string message)
            : base(message) {
        }

        public CarCurrentlyRentedException(string message, Exception exception)
            : base(message, exception) {
        }
    }
}

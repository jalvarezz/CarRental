using CarRental.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Admin.Support
{
    public class CarEventArgs : EventArgs
    {
        public Car Car { get; set; }
        public bool IsNew { get; set; }

        public CarEventArgs(Car car, bool isNew)
        {
            Car = car;
            IsNew = IsNew;
        }
    }
}

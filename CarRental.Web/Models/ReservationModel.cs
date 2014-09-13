using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.Web.Models
{
    public class ReservationModel
    {
        public int Car { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
﻿using CarRental.Business.Entities;
using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Common
{
    public interface ICarRentalEngine : IBusinessEngine
    {
        bool IsCarAvailableForRental(int carId, DateTime pickupDate, DateTime returnDate, IEnumerable<Rental> rentedCars, IEnumerable<Reservation> reservedCars);
    }
}

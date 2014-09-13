using CarRental.Client.Contracts;
using Core.Common.ServiceModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Proxies
{
    [Export(typeof(IInventoryService))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class InventoryClient : UserClientBase<IInventoryService>, IInventoryService
    {
        #region IInventoryService Members

        public Entities.Car GetCar(int carId)
        {
            return Channel.GetCar(carId);
        }

        public Entities.Car[] GetAllCars()
        {
            return Channel.GetAllCars();
        }

        public Entities.Car UpdateCar(Entities.Car car)
        {
            return Channel.UpdateCar(car);
        }

        public void DeleteCar(int carId)
        {
            Channel.DeleteCar(carId);
        }

        public Entities.Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate)
        {
            return Channel.GetAvailableCars(pickupDate, returnDate);
        }

        public Task<Entities.Car> GetCarAsync(int carId)
        {
            return Channel.GetCarAsync(carId);
        }

        public Task<Entities.Car[]> GetAllCarsAsync()
        {
            return Channel.GetAllCarsAsync();
        }

        public Task<Entities.Car> UpdateCarAsync(Entities.Car car)
        {
            return Channel.UpdateCarAsync(car);
        }

        public Task DeleteCarAsync(int carId)
        {
            return Channel.DeleteCarAsync(carId);
        }

        public Task<Entities.Car[]> GetAvailableCarsAsync(DateTime pickupDate, DateTime returnDate)
        {
            return Channel.GetAvailableCarsAsync(pickupDate, returnDate);
        }

        #endregion
    }
}

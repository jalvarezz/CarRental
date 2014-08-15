using CarRental.Client.Entities;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Contracts
{
    [ServiceContract]
    public interface IInventoryService : IServiceContract {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Car GetCar(int carId);

        [OperationContract]
        Car[] GetAllCars();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Car UpdateCar(Car car);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCar(int carId);

        [OperationContract]
        Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate);

        [OperationContract]
        Task<Car> GetCarAsync(int carId);

        [OperationContract]
        Task<Car[]> GetAllCarsAsync();

        [OperationContract]
        Task<Car> UpdateCarAsync(Car car);

        [OperationContract]
        Task DeleteCarAsync(int carId);

        [OperationContract]
        Task<Car[]> GetAvailableCarsAsync(DateTime pickupDate, DateTime returnDate);
    }
}

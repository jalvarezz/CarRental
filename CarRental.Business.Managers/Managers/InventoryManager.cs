﻿using CarRental.Business.Common;
using CarRental.Business.Contracts;
using CarRental.Business.Entities;
using CarRental.Common;
using CarRental.Data.Contracts.Repository_Interfaces;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Managers {
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall, 
                     ConcurrencyMode=ConcurrencyMode.Multiple, 
                     ReleaseServiceInstanceOnTransactionComplete=false)]
    public class InventoryManager : ManagerBase, IInventoryService {
        public InventoryManager() {
            
        }

        public InventoryManager(IDataRepositoryFactory dataRepositoryFactory) {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        public InventoryManager(IBusinessEngineFactory businessEngineFactory) {
            _BusinessEngineFactory = businessEngineFactory;
        }

        public InventoryManager(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory) {
            _DataRepositoryFactory = dataRepositoryFactory;
            _BusinessEngineFactory = businessEngineFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        [Import]
        IBusinessEngineFactory _BusinessEngineFactory;

        #region IInventoryService Members

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public Entities.Car GetCar(int carId) {
            return ExecuteFaultHandledOperation(() => {
                ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();

                Car carEntity = carRepository.Get(carId);

                if (carEntity == null) {
                    NotFoundException ex = new NotFoundException(string.Format("Car with ID of {0} is not in the database.", carId));

                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return carEntity;
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public Entities.Car[] GetAllCars() {
            return ExecuteFaultHandledOperation(() => {
                ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
                IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

                IEnumerable<Car> cars = carRepository.Get();
                IEnumerable<Rental> renterCars = rentalRepository.GetCurrentlyRentedCars();

                foreach (Car car in cars) {
                    Rental rentedCar = renterCars.Where(item => item.CarId == car.CarId).FirstOrDefault();
                    car.CurrentlyRented = (rentedCar != null);
                }

                return cars.ToArray();
            });
        }

        [OperationBehavior(TransactionScopeRequired=true)]
        //[PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        public Car UpdateCar(Car car) {
            return ExecuteFaultHandledOperation(() => {
                ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();

                Car updatedEntity = null;

                if (car.CarId == 0)
                    updatedEntity = carRepository.Add(car);
                else
                    updatedEntity = carRepository.Update(car);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        //[PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        public void DeleteCar(int carId) {
            ExecuteFaultHandledOperation(() => {
                ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();

                carRepository.Remove(carId);
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate) {
            return ExecuteFaultHandledOperation(() => {
                ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
                IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();
                IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();

                ICarRentalEngine carRentalEngine = _BusinessEngineFactory.GetBusinessEngine<ICarRentalEngine>();
                
                IEnumerable<Car> allCars = carRepository.Get();
                IEnumerable<Rental> rentedCars = rentalRepository.GetCurrentlyRentedCars();
                IEnumerable<Reservation> reservedCars = reservationRepository.Get();

                List<Car> availableCars = new List<Car>();

                foreach (Car car in allCars) {
                    if (carRentalEngine.IsCarAvailableForRental(car.CarId, pickupDate, returnDate, rentedCars, reservedCars))
                        availableCars.Add(car);
                }

                return availableCars.ToArray();
            });
        }

        #endregion
    }
}

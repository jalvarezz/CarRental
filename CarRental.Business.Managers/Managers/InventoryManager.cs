using CarRental.Business.Common;
using CarRental.Business.Contracts;
using CarRental.Business.Entities;
using CarRental.Common;
using CarRental.Data.Contracts;
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
        public InventoryManager(IRepositoryFactory repositoryFactory)
            : base()
        {
            _RepositoryFactory = repositoryFactory;
        }

        public InventoryManager(IBusinessEngineFactory businessEngineFactory)
            : base()
        {
            _BusinessEngineFactory = businessEngineFactory;
        }

        public InventoryManager(IRepositoryFactory repositoryFactory, IBusinessEngineFactory businessEngineFactory) : base() {
            _RepositoryFactory = repositoryFactory;
            _BusinessEngineFactory = businessEngineFactory;
        }

        IRepositoryFactory _RepositoryFactory;
        IBusinessEngineFactory _BusinessEngineFactory;

        #region IInventoryService Members

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public Entities.Car GetCar(int carId) {
            return ExecuteFaultHandledOperation(() => {
                ICarRepository carRepository = _RepositoryFactory.BuildCustomRepository<ICarRepository>();

                Car carEntity = carRepository.GetById(carId);

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
                using (ICarRepository carRepository = _RepositoryFactory.BuildCustomRepository<ICarRepository>())
                {
                    using (IRentalRepository rentalRepository = _RepositoryFactory.BuildCustomRepository<IRentalRepository>())
                    {
                        IEnumerable<Car> cars = carRepository.Get();
                        IEnumerable<Rental> rentedCars = rentalRepository.GetCurrentlyRentedCars();

                        foreach (Car car in cars)
                        {
                            Rental rentedCar = rentedCars.Where(item => item.CarId == car.CarId).FirstOrDefault();
                            car.CurrentlyRented = (rentedCar != null);
                        }

                        return cars.ToArray();
                    }
                }
            });
        }

        [OperationBehavior(TransactionScopeRequired=true)]
        //[PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        public Car UpdateCar(Car car) {
            return ExecuteFaultHandledOperation(() => {
                Car updatedEntity = null;

                using (ICarRepository carRepository = _RepositoryFactory.BuildCustomRepository<ICarRepository>())
                {
                    if (car.CarId == 0)
                        updatedEntity = carRepository.Insert(car);
                    else
                        updatedEntity = carRepository.Update(car);
                }

                return updatedEntity;

            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        //[PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        public void DeleteCar(int carId) {
            ExecuteFaultHandledOperation(() => {
                ICarRepository carRepository = _RepositoryFactory.BuildCustomRepository<ICarRepository>();

                carRepository.Delete(carId);
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate) {
            return ExecuteFaultHandledOperation(() => {
                ICarRepository carRepository = _RepositoryFactory.BuildCustomRepository<ICarRepository>();
                IRentalRepository rentalRepository = _RepositoryFactory.BuildCustomRepository<IRentalRepository>();
                IReservationRepository reservationRepository = _RepositoryFactory.BuildCustomRepository<IReservationRepository>();

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

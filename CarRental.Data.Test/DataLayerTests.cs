using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Common.Contracts;
using CarRental.Business.Bootstrapper;
using CarRental.Business.Entities;
using System.Collections.Generic;
using System.Collections;
using Moq;
using CarRental.Data.Contracts;
using StructureMap;

namespace CarRental.Data.Test {
    [TestClass]
    public class DataLayerTests {
        [TestInitialize]
        public void Initialize() {
            StructureMapLoader.Init();
        }

        [TestMethod]
        public void test_repository_usage() {
            RepositoryTestClass repositoryTest = ObjectFactory.GetInstance<RepositoryTestClass>();

            IEnumerable<Car> cars = repositoryTest.GetCars();
            IEnumerable<Rental> rentals = repositoryTest.GerRentals();
            IEnumerable<Reservation> reservations = repositoryTest.GetReservations();
            IEnumerable<Account> accounts = repositoryTest.GetAccounts();

            Assert.IsTrue(cars != null);
            Assert.IsTrue(rentals != null);
            Assert.IsTrue(reservations != null);
            Assert.IsTrue(accounts != null);
        }

        [TestMethod]
        public void test_repository_factory_usage() {
            RepositoryFactoryTestClass factoryTest = ObjectFactory.GetInstance<RepositoryFactoryTestClass>();

            IEnumerable<Car> cars = factoryTest.GetCars();

            Assert.IsTrue(cars != null);
        }

        [TestMethod]
        public void test_factory_mocking1() {
            List<Car> cars = new List<Car>(){
                new Car() { CarId = 1, Description = "Mustang" },
                new Car() { CarId = 2, Description = "Corvette" }
            };

            Mock<IRepositoryFactory> mockDataRepository = new Mock<IRepositoryFactory>();
            mockDataRepository.Setup(obj => obj.BuildCustomRepository<ICarRepository>().Get()).Returns(cars);

            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);

            IEnumerable<Car> ret = factoryTest.GetCars();

            Assert.IsTrue(ret == cars);
        }

        [TestMethod]
        public void test_factory_mocking2() {
            List<Car> cars = new List<Car>(){
                new Car() { CarId = 1, Description = "Mustang" },
                new Car() { CarId = 2, Description = "Corvette" }
            };

            Mock<ICarRepository> mockCarRepository = new Mock<ICarRepository>();
            mockCarRepository.Setup(obj => obj.Get()).Returns(cars);

            Mock<IRepositoryFactory> mockDataRepository = new Mock<IRepositoryFactory>();
            mockDataRepository.Setup(obj => obj.BuildCustomRepository<ICarRepository>()).Returns(mockCarRepository.Object);

            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);

            IEnumerable<Car> ret = factoryTest.GetCars();

            Assert.IsTrue(ret == cars);
        }

        [TestMethod]
        public void test_repository_mocking() {
            List<Car> cars = new List<Car>(){
                new Car() { CarId = 1, Description = "Mustang" },
                new Car() { CarId = 2, Description = "Corvette" }
            };

            Mock<ICarRepository> mockCarRepository = new Mock<ICarRepository>();
            mockCarRepository.Setup(obj => obj.Get()).Returns(cars);

            RepositoryTestClass repositoryTest = new RepositoryTestClass(mockCarRepository.Object);

            IEnumerable<Car> ret = repositoryTest.GetCars();

            Assert.IsTrue(ret == cars);
        }
    }

    public class RepositoryTestClass {
        public RepositoryTestClass(){
            
        }

        public RepositoryTestClass(ICarRepository carRepository)
        {
            _CarRepository = carRepository;
        }

        public RepositoryTestClass(ICarRepository carRepository, 
                                   IRentalRepository rentalRepository,
                                   IReservationRepository reservationRepository, 
                                   IAccountRepository accountRepository) {
            _CarRepository = carRepository;
            _RentalRepository = rentalRepository;
            _ReservationRepository = reservationRepository;
            _AccountRepository = accountRepository;
        }

        ICarRepository _CarRepository;
        IRentalRepository _RentalRepository;
        IReservationRepository _ReservationRepository;
        IAccountRepository _AccountRepository;

        public IEnumerable<Car> GetCars() {
            IEnumerable<Car> cars = _CarRepository.Get();

            return cars;
        }

        public IEnumerable<Rental> GerRentals()
        {
            IEnumerable<Rental> rentals = _RentalRepository.Get();

            return rentals;
        }

        public IEnumerable<Reservation> GetReservations()
        {
            IEnumerable<Reservation> reservations = _ReservationRepository.Get();

            return reservations;
        }

        public IEnumerable<Account> GetAccounts()
        {
            IEnumerable<Account> account = _AccountRepository.Get();

            return account;
        }
    }

    public class RepositoryFactoryTestClass {
        public RepositoryFactoryTestClass() {
        }

        public RepositoryFactoryTestClass(IRepositoryFactory dataRepositoryFactory) {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        IRepositoryFactory _DataRepositoryFactory;

        public IEnumerable<Car> GetCars() {
            ICarRepository carRepository = _DataRepositoryFactory.BuildCustomRepository<ICarRepository>();

            IEnumerable<Car> cars = carRepository.Get();

            return cars;
        }
    }
}

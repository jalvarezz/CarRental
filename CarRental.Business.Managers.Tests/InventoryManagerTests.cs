using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRental.Business.Entities;
using Moq;
using Core.Common.Contracts;
using System.Security.Principal;
using CarRental.Common;
using System.Threading;
using CarRental.Data.Contracts;

namespace CarRental.Business.Managers.Tests
{
    [TestClass]
    public class InventoryManagerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            GenericPrincipal principal = new GenericPrincipal(
                new GenericIdentity("Juan"), new string[] { "Administrators" });

            Thread.CurrentPrincipal = principal;
        }

        [TestMethod]
        public void UpdateCar_add_new()
        {
            Car newCar = new Car();
            Car addedCar = new Car() { CarId = 1 };

            Mock<IRepositoryFactory> mockRepositoryFactory = new Mock<IRepositoryFactory>();
            mockRepositoryFactory.Setup(obj => obj.BuildCustomRepository<ICarRepository>().Insert(newCar)).Returns(addedCar);

            InventoryManager manager = new InventoryManager(mockRepositoryFactory.Object);

            Car results = manager.UpdateCar(newCar);

            Assert.IsTrue(results == addedCar);
        }

        [TestMethod]
        public void UpdateCar_update_existing()
        {
            Car existingCar = new Car() { CarId = 1 };
            Car updatedCar = new Car() { CarId = 1 };

            Mock<IRepositoryFactory> mockRepositoryFactory = new Mock<IRepositoryFactory>();
            mockRepositoryFactory.Setup(obj => obj.BuildCustomRepository<ICarRepository>().Update(existingCar)).Returns(updatedCar);

            InventoryManager manager = new InventoryManager(mockRepositoryFactory.Object);

            Car results = manager.UpdateCar(existingCar);

            Assert.IsTrue(results == updatedCar);
        }
    }
}

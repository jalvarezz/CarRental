using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRental.Business.Entities;
using Moq;
using Core.Common.Contracts;
using CarRental.Business;
using CarRental.Data.Contracts;

namespace CarRental.Business.Tests
{
    [TestClass]
    public class CarRentalEngineTests
    {
        [TestMethod]
        public void IsCarCurrentlyRented_any_account()
        {
            Rental rental = new Rental()
            {
                CarId = 1
            };

            Mock<IRentalRepository> mockRentalRepository = new Mock<IRentalRepository>();
            mockRentalRepository.Setup(obj => obj.GetCurrentRentalByCar(1)).Returns(rental);

            Mock<IRepositoryFactory> mockRepositoryFactory = new Mock<IRepositoryFactory>();
            mockRepositoryFactory.Setup(obj => obj.BuildCustomRepository<IRentalRepository>()).Returns(mockRentalRepository.Object);

            CarRentalEngine engine = new CarRentalEngine(mockRepositoryFactory.Object);

            bool try1 = engine.IsCarCurrentlyRented(2);
            bool try2 = engine.IsCarCurrentlyRented(1);

            Assert.IsFalse(try1);
            Assert.IsTrue(try2);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRental.Client.Entities;
using System.Collections.Generic;
using Moq;
using Core.Common.Contracts;
using CarRental.Client.Contracts;
using CarRental.Admin.ViewModels;
using System.Collections.ObjectModel;

namespace CarRental.Admin.Tests.ViewModels
{
    [TestClass]
    public class MaintainCarsViewModelTests
    {
        [TestMethod]
        public void TestViewLoaded()
        {
            Car[] data = new List<Car>(){
                new Car() { CarId = 1 },
                new Car() { CarId = 2 }
            }.ToArray();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IInventoryService>().GetAllCars()).Returns(data);

            MaintainCarsViewModel viewModel = new MaintainCarsViewModel(mockServiceFactory.Object);

            Assert.IsTrue(viewModel.Cars == null);

            object loaded = viewModel.ViewLoaded;

            Assert.IsTrue(viewModel.Cars != null && viewModel.Cars.Count == data.Length && viewModel.Cars[0] == data[0]);
        }

        [TestMethod]
        public void TestCurrentCarSetting()
        {
            Car car = new Car() { CarId = 1 };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            MaintainCarsViewModel viewModel = new MaintainCarsViewModel(mockServiceFactory.Object);

            Assert.IsTrue(viewModel.CurrentCarViewModel == null);

            viewModel.EditCarCommand.Execute(car);

            Assert.IsTrue(viewModel.CurrentCarViewModel != null && viewModel.CurrentCarViewModel.Car.CarId == car.CarId);
        }

        [TestMethod]
        public void TestEditCarCommand()
        {
            Car car = new Car() { CarId = 1, Color = "White", Year = 2013, RentalPrice = 100, Description = "Just some car" };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            MaintainCarsViewModel viewModel = new MaintainCarsViewModel(mockServiceFactory.Object);

            viewModel.Cars = new ObservableCollection<Car>()
                {
                    car
                };

            Assert.IsTrue(viewModel.Cars[0].Color == "White");
            Assert.IsTrue(viewModel.CurrentCarViewModel == null);

            viewModel.EditCarCommand.Execute(car);

            Assert.IsTrue(viewModel.CurrentCarViewModel != null);

            mockServiceFactory.Setup(mock => mock.CreateClient<IInventoryService>().UpdateCar(It.IsAny<Car>())).Returns(viewModel.CurrentCarViewModel.Car);

            viewModel.CurrentCarViewModel.Car.Color = "Black";
            viewModel.CurrentCarViewModel.SaveCommand.Execute(null);

            Assert.IsTrue(viewModel.Cars[0].Color == "Black");
        }
    }
}

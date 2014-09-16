using CarRental.Admin.Support;
using CarRental.Client.Contracts;
using CarRental.Client.Entities;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Admin.ViewModels
{
    public class EditCarViewModel : ViewModelBase
    {
        public EditCarViewModel(IServiceFactory serviceFactory, Car car)
        {
            _ServiceFactory = serviceFactory;
            _Car = new Car()
            {
                CarId = car.CarId,
                Description = car.Description,
                Color = car.Color,
                Year = car.Year,
                RentalPrice = car.RentalPrice
            };

            _Car.CleanAll();

            SaveCommand = new DelegateCommand<object>(OnSaveCommandExecute, OnSaveCommandCanExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelCommandExecute);
        }

        IServiceFactory _ServiceFactory;
        Car _Car;

        public Car Car
        {
            get { return _Car; }
        }

        public DelegateCommand<object> SaveCommand { get; private set; }
        public DelegateCommand<object> CancelCommand { get; private set; }

        public event EventHandler<CarEventArgs> CarUpdated;
        public event EventHandler CancelEditCar;

        protected override void AddModels(List<Core.Common.Core.ObjectBase> models)
        {
            models.Add(Car);
        }

        void OnSaveCommandExecute(object arg)
        {
            ValidateModel();

            if (IsValid)
            {
                WithClient<IInventoryService>(_ServiceFactory.CreateClient<IInventoryService>(), inventoryClient =>
                {
                    bool isNew = (_Car.CarId == 0);

                    var savedCar = inventoryClient.UpdateCar(_Car);
                    if(savedCar != null)
                    {
                        if (CarUpdated != null)
                        {
                            CarUpdated(this, new CarEventArgs(savedCar, isNew));
                        }
                    }
                });
            }
        }

        bool OnSaveCommandCanExecute(object arg)
        {
            return _Car.IsDirty;
        }

        void OnCancelCommandExecute(object arg)
        {
            if (CancelEditCar != null)
                CancelEditCar(this, EventArgs.Empty);
        }
    }
}

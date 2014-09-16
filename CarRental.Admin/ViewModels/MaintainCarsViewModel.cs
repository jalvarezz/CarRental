using Core.Common.Contracts;
using Core.Common.UI.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CarRental.Client.Entities;
using CarRental.Client.Contracts;
using Core.Common;
using System.ServiceModel;

namespace CarRental.Admin.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MaintainCarsViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public MaintainCarsViewModel(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;

            EditCarCommand = new DelegateCommand<Car>(OnEditCarCommand);
            DeleteCarCommand = new DelegateCommand<Car>(OnDeleteCarCommand);
            AddCarCommand = new DelegateCommand<object>(OnAddCarCommand);
        }

        IServiceFactory _ServiceFactory;

        public override string ViewTitle
        {
            get { return "Maintain Cars"; }
        }

        ObservableCollection<Car> _Cars;
        EditCarViewModel _CurrentCarViewModel;

        public DelegateCommand<Car> EditCarCommand { get; private set; }
        public DelegateCommand<Car> DeleteCarCommand { get; private set; }
        public DelegateCommand<object> AddCarCommand { get; private set; }

        public event CancelEventHandler ConfirmDelete;
        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;

        protected virtual void OnConfirmDelete(CancelEventArgs e)
        {
            if (ConfirmDelete != null)
                ConfirmDelete(this, e);
        }

        protected virtual void OnErrorOccured(ErrorMessageEventArgs e)
        {
            if (ErrorOccured != null)
                ErrorOccured(this, e);
        }

        public ObservableCollection<Car> Cars
        {
            get { return _Cars; }
            set
            {
                _Cars = value;
                OnPropertyChanged(false);
            }
        }

        public EditCarViewModel CurrentCarViewModel
        {
            get { return _CurrentCarViewModel; }
            set
            {
                if (_CurrentCarViewModel != value)
                {
                    _CurrentCarViewModel = value;
                    OnPropertyChanged(false);
                }
            }
        }

        protected override void OnViewLoaded()
        {
            _Cars = new ObservableCollection<Car>();

            try
            {
                WithClient<IInventoryService>(_ServiceFactory.CreateClient<IInventoryService>(), inventoryClient =>
                {
                    Car[] cars = inventoryClient.GetAllCars();

                    if (cars != null)
                    {
                        foreach (Car car in cars)
                            _Cars.Add(car);
                    }
                });
            }
            catch (FaultException ex)
            {
                if (ErrorOccured != null)
                    ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
            }
            catch (Exception ex)
            {
                if (ErrorOccured != null)
                    ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
            }
        }

        private void OnAddCarCommand(object arg)
        {
            Car car = new Car();

            CurrentCarViewModel = new EditCarViewModel(_ServiceFactory, car);
            CurrentCarViewModel.CarUpdated += CurrentCarViewModel_CarUpdated;
            CurrentCarViewModel.CancelEditCar += CurrentCarViewModel_CancelEditCar;
        }

        private void OnEditCarCommand(Car car)
        {
            if (car != null)
            {
                CurrentCarViewModel = new EditCarViewModel(_ServiceFactory, car);
                CurrentCarViewModel.CarUpdated += CurrentCarViewModel_CarUpdated;
                CurrentCarViewModel.CancelEditCar += CurrentCarViewModel_CancelEditCar;
            }
        }

        private void OnDeleteCarCommand(Car car)
        {
            bool carIsRented = false;

            // check to see if car is currently rented
            WithClient<IRentalService>(_ServiceFactory.CreateClient<IRentalService>(), rentalClient =>
            {
                carIsRented = rentalClient.IsCarCurrentlyRented(car.CarId);
            });

            if (!carIsRented)
            {
                CancelEventArgs args = new CancelEventArgs();
                if (ConfirmDelete != null)
                    ConfirmDelete(this, args);

                if (!args.Cancel)
                {
                    try
                    {
                        WithClient<IInventoryService>(_ServiceFactory.CreateClient<IInventoryService>(), inventoryClient =>
                        {
                            inventoryClient.DeleteCar(car.CarId);
                            _Cars.Remove(car);
                        });
                    }
                    catch (FaultException ex)
                    {
                        if (ErrorOccured != null)
                            ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                    }
                    catch (Exception ex)
                    {
                        if (ErrorOccured != null)
                            ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                    }
                }
            }
            else
            {
                if (ErrorOccured != null)
                    ErrorOccured(this, new ErrorMessageEventArgs("Cannot delete this car. It is currently rented"));
            }
        }

        void CurrentCarViewModel_CarUpdated(object sender, Support.CarEventArgs e)
        {
            if (!e.IsNew)
            {
                Car car = _Cars.Where(item => item.CarId == e.Car.CarId).FirstOrDefault();
                if (car != null)
                {
                    car.Description = e.Car.Description;
                    car.Color = e.Car.Color;
                    car.Year = e.Car.Year;
                    car.RentalPrice = e.Car.RentalPrice;
                }
            }
            else
            {
                _Cars.Add(e.Car);
            }

            CurrentCarViewModel = null;
        }

        void CurrentCarViewModel_CancelEditCar(object sender, EventArgs e)
        {
            CurrentCarViewModel = null;
        }
    }
}

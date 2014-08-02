using Core.Common.Core;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Entities {
    public class Car : ObjectBase {
        int _CarId;
        string _Description;
        string _Color;
        int _Year;
        decimal _RentalPrice;
        bool _CurrentlyRented;

        public int CarId {
            get { return _CarId; }
            set {
                if (_CarId != value) 
                {
                    _CarId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description {
            get { return _Description; }
            set {
                if (_Description != value) {
                    _Description = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Color {
            get { return _Color; }
            set {
                if (_Color != value) {
                    _Color = value;
                    OnPropertyChanged();
                }
            }
        }
              
        public int Year {
            get { return _Year; }
            set {
                if (_Year != value) {
                    _Year = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal RentalPrice {
            get { return _RentalPrice; }
            set {
                if (_RentalPrice != value) {
                    _RentalPrice = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool CurrentlyRented {
            get { return _CurrentlyRented; }
            set {
                if (_CurrentlyRented != value) {
                    _CurrentlyRented = value;
                    OnPropertyChanged();
                }
            }
        }

        class CarValidator : AbstractValidator<Car> {
            public CarValidator() {
                RuleFor(obj => obj.Description).NotEmpty();
                RuleFor(obj => obj.Color).NotEmpty();
                RuleFor(obj => obj.RentalPrice).GreaterThan(0);
                RuleFor(obj => obj.Year).GreaterThan(2000).LessThanOrEqualTo(DateTime.Now.Year);
            }
        }

        protected override IValidator GetValidator() {
            return new CarValidator();
        }
    }
}

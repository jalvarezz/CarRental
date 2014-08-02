using Core.Common.Core;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Entities {
    public class Rental : ObjectBase {
        int _RentalId;
        int _AccountId;
        int _CarId;
        DateTime _DateRented;
        DateTime _DateDue;
        DateTime? _DateReturned;

        public int RentalId {
            get { return _RentalId; }
            set {
                if (_RentalId != value) 
                {
                    _RentalId = value;
                    OnPropertyChanged();
                }
            }
        }

        public int AccountId {
            get { return _AccountId; }
            set {
                if (_AccountId != value) {
                    _AccountId = value;
                    OnPropertyChanged();
                }
            }
        }

        public int CarId {
            get { return _CarId; }
            set {
                if (_CarId != value) {
                    _CarId = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DateRented {
            get { return _DateRented; }
            set {
                if (_DateRented != value) {
                    _DateRented = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DateDue {
            get { return _DateDue; }
            set {
                if (_DateDue != value) {
                    _DateDue = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? DateReturned {
            get { return _DateReturned; }
            set {
                if (_DateReturned != value) {
                    _DateReturned = value;
                    OnPropertyChanged();
                }
            }
        }

        class RentalValidator : AbstractValidator<Rental> {
            public RentalValidator() {
            }
        }

        protected override IValidator GetValidator() {
            return new RentalValidator();
        }
    }
}

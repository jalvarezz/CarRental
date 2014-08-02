using Core.Common.Core;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Entities {
    public class Reservation : ObjectBase {
        int _ReservationId;
        int _AccountId;
        int _CarId;
        DateTime _ReturnDate;
        DateTime _RentalDate;

        public int ReservationId {
            get { return _ReservationId; }
            set {
                if (_ReservationId != value) 
                {
                    _ReservationId = value;
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

        public DateTime ReturnDate {
            get { return _ReturnDate; }
            set {
                if (_ReturnDate != value) {
                    _ReturnDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime RentalDate {
            get { return _RentalDate; }
            set {
                if (_RentalDate != value) {
                    _RentalDate = value;
                    OnPropertyChanged();
                }
            }
        }

        class ReservationValidator : AbstractValidator<Reservation> {
            public ReservationValidator() {
            }
        }

        protected override IValidator GetValidator() {
            return new ReservationValidator();
        }
    }
}

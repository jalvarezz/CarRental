using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Entities {
    
    public class Account : ObjectBase {

        int _AccountId;
        string _LoginEmail;
        string _FirstName;
        string _LastName;
        string _Address;
        string _City;
        string _State;
        string _ZipCode;
        string _CreditCard;
        string _ExpDate;

        public int AccountId {
            get { return _AccountId; }
            set {
                if (_AccountId != value) {
                    _AccountId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LoginEmail {
            get { return _LoginEmail; }
            set {
                if (_LoginEmail != value) {
                    _LoginEmail = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName {
            get { return _FirstName; }
            set {
                if (_FirstName != value) {
                    _FirstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LastName {
            get { return _LastName; }
            set {
                if (_LastName != value) {
                    _LastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Address {
            get { return _Address; }
            set {
                if (_Address != value) {
                    _Address = value;
                    OnPropertyChanged();
                }
            }
        }

        public string City {
            get { return _City; }
            set {
                if (_City != value) {
                    _City = value;
                    OnPropertyChanged();
                }
            }
        }

        public string State {
            get { return _State; }
            set {
                if (_State != value) {
                    _State = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ZipCode {
            get { return _ZipCode; }
            set {
                if (_ZipCode != value) {
                    _ZipCode = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CreditCard {
            get { return _CreditCard; }
            set {
                if (_CreditCard != value) {
                    _CreditCard = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ExpDate {
            get { return _ExpDate; }
            set {
                if (_ExpDate != value) {
                    _ExpDate = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}

using Core.Common.Contracts;
using Core.Common.Core;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Entities {
    [DataContract]
    public class Rental : EntityBase, IIdentifiableEntity, IAccountOwnedEntity {
        [DataMember]
        public int RentalId { get; set; }

        #region IAccountOwnedEntity Member

        [DataMember]
        public int AccountId { get; set; }

        #endregion

        [DataMember]
        public int CarId { get; set; }

        [DataMember]
        public DateTime DateRented { get; set; }

        [DataMember]
        public DateTime DateDue { get; set; }

        [DataMember]
        public DateTime? DateReturned { get; set; }

        #region IIdentifiableEntity Members

        public int EntityId {
            get { return RentalId; }
            set { RentalId = value; }
        }

        #endregion
    }
}

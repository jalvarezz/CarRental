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
    public class Reservation : EntityBase, IIdentifiableEntity, IAccountOwnedEntity {
        [DataMember]
        public int ReservationId { get; set; }

        #region IAccountOwnedEntity Members

        [DataMember]
        public int AccountId { get; set; }

        #endregion

        [DataMember]
        public int CarId { get; set; }

        [DataMember]
        public DateTime ReturnDate { get; set; }

        [DataMember]
        public DateTime RentalDate { get; set; }

        #region IIdentifiableEntity Members

        public int EntityId {
            get { return ReservationId; }
            set { ReservationId = value; }
        }

        #endregion
    }
}

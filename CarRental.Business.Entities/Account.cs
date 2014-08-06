using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Entities {
    [DataContract]
    public class Account : EntityBase, IIdentifiableEntity, IAccountOwnedEntity
    {
        #region IAccountOwnedEntity Member

        [DataMember]
        public int AccountId { get; set; }

        #endregion

        [DataMember]
        public string LoginEmail { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string ZipCode { get; set; }

        [DataMember]
        public string CreditCard { get; set; }

        [DataMember]
        public string ExpDate { get; set; }

        #region IIdentifiableEntity Member

        public int EntityId {
            get { return AccountId; }
            set { AccountId = value; }
        }

        #endregion
    }
}

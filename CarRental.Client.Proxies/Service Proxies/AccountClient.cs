using CarRental.Client.Contracts;
using Core.Common.ServiceModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Proxies
{
    [Export(typeof(IAccountService))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class AccountClient : UserClientBase<IAccountService>, IAccountService
    {
        #region IAccountService Members

        public Entities.Account GetCustomerAccountInfo(string loginEmail)
        {
            return Channel.GetCustomerAccountInfo(loginEmail);
        }

        public void UpdateCustomerAccountInfo(Entities.Account account)
        {
            Channel.UpdateCustomerAccountInfo(account);
        }

        public Task<Entities.Account> GetCustomerAccountInfoAsync(string loginEmail)
        {
            return Channel.GetCustomerAccountInfoAsync(loginEmail);
        }

        public Task UpdateCustomerAccountInfoAsync(Entities.Account account)
        {
            return Channel.UpdateCustomerAccountInfoAsync(account);
        }

        #endregion
    }
}

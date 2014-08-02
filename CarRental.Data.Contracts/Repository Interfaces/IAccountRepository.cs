using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Business.Entities;

namespace CarRental.Data.Contracts.Repository_Interfaces {
    public interface IAccountRepository : IDataRepository<Account> {
        Account GetByLogin(string login);
    }
}

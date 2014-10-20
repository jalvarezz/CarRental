using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Business.Entities;
using System.Data.Entity;
using CarRental.Data.Contracts;

namespace CarRental.Data {
    public class AccountRepository : Repository<Account>, IAccountRepository {

        public AccountRepository(DbContext context)
            : base(context)
        {
        }

        public Account GetByLogin(string login) {
            return (from e in _Context.Set<Account>()
                        where e.LoginEmail == login
                        select e).FirstOrDefault();
        }
    }
}

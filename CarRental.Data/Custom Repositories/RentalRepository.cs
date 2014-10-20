using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Business.Entities;
using Core.Common.Extensions;
using CarRental.Data.Contracts;
using System.Data.Entity;

namespace CarRental.Data
{
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        public RentalRepository(DbContext context)
            : base(context)
        {
        }

        public IEnumerable<Rental> GetRentalHistoryByAccount(int accountId)
        {
            var query = from e in _Context.Set<Rental>()
                        where e.AccountId == accountId
                        select e;

            return query.ToFullyLoaded();
        }

        public IEnumerable<CustomerRentalInfo> GetCurrentCustomerRentalInfo()
        {
            var query = from r in _Context.Set<Rental>()
                        where r.DateReturned == null
                        join a in _Context.Set<Account>() on r.AccountId equals a.AccountId
                        join c in _Context.Set<Car>() on r.CarId equals c.CarId
                        select new CustomerRentalInfo()
                        {
                            Customer = a,
                            Car = c,
                            Rental = r
                        };

            return query.ToFullyLoaded();
        }

        public IEnumerable<Rental> GetRentalHistoryByCar(int carId)
        {
            var query = from r in _Context.Set<Rental>()
                        where r.CarId == carId
                        select r;

            return query.ToFullyLoaded();
        }

        public Rental GetCurrentRentalByCar(int carId)
        {
            var query = from r in _Context.Set<Rental>()
                        where r.CarId == carId && r.DateReturned == null
                        select r;

            return query.FirstOrDefault();
        }

        public IEnumerable<Rental> GetCurrentlyRentedCars()
        {
            var query = from r in _Context.Set<Rental>()
                        where r.DateReturned == null
                        select r;

            return query.ToFullyLoaded();
        }
    }
}

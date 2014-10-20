using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Business.Entities;
using Core.Common.Extensions;
using CarRental.Data.Contracts;
using System.Data.Entity;

namespace CarRental.Data {
    public class ReservationRepository : Repository<Reservation>, IReservationRepository {

        public ReservationRepository(DbContext context)
            : base(context)
        {
        }

        public IEnumerable<Reservation> GetReservationsByPickupDate(DateTime pickupDate)
        {
            var query = from r in _Context.Set<Reservation>()
                        where r.RentalDate < pickupDate
                        select r;

            return query.ToFullyLoaded();
        }

        public IEnumerable<Contracts.CustomerReservationInfo> GetCurrentCustomerReservationInfo()
        {
            var query = from r in _Context.Set<Reservation>()
                        join a in _Context.Set<Account>() on r.AccountId equals a.AccountId
                        join c in _Context.Set<Car>() on r.CarId equals c.CarId
                        select new CustomerReservationInfo()
                        {
                            Customer = a,
                            Car = c,
                            Reservation = r
                        };

            return query.ToFullyLoaded();
        }

        public IEnumerable<Contracts.CustomerReservationInfo> GetCustomerOpenReservationInfo(int accountId)
        {
            var query = from r in _Context.Set<Reservation>()
                        join a in _Context.Set<Account>() on r.AccountId equals a.AccountId
                        join c in _Context.Set<Car>() on r.CarId equals c.CarId
                        where r.AccountId == accountId
                        select new CustomerReservationInfo()
                        {
                            Customer = a,
                            Car = c,
                            Reservation = r
                        };

            return query.ToFullyLoaded();
        }
    }
}

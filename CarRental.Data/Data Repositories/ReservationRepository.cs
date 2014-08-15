using Core.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Business.Entities;
using CarRental.Data.Contracts.Repository_Interfaces;
using Core.Common.Extensions;
using CarRental.Data.Contracts;

namespace CarRental.Data {
    [Export(typeof(IReservationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ReservationRepository : DataRepositoryBase<Reservation>, IReservationRepository {
        protected override Reservation AddEntity(CarRentalContext entityContext, Reservation entity) {
            return entityContext.ReservationSet.Add(entity);
        }

        protected override Reservation UpdateEntity(CarRentalContext entityContext, Reservation entity) {
            return (from e in entityContext.ReservationSet
                    where e.ReservationId == entity.ReservationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Reservation> GetEntities(CarRentalContext entityContext) {
            return from e in entityContext.ReservationSet
                   select e;
        }

        protected override Reservation GetEntity(CarRentalContext entityContext, object id) {
            int idToFind = (int)id;

            var query = from e in entityContext.ReservationSet
                        where e.ReservationId == idToFind
                        select e;

            var result = query.FirstOrDefault();

            return result;
        }

        public IEnumerable<Reservation> GetReservationsByPickupDate(DateTime pickupDate) {
            using (CarRentalContext entityContext = new CarRentalContext()) {
                var query = from r in entityContext.ReservationSet
                            where r.RentalDate < pickupDate
                            select r;

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<Contracts.CustomerReservationInfo> GetCurrentCustomerReservationInfo() {
            using (CarRentalContext entityContext = new CarRentalContext()) {
                var query = from r in entityContext.ReservationSet
                            join a in entityContext.AccountSet on r.AccountId equals a.AccountId
                            join c in entityContext.CarSet on r.CarId equals c.CarId
                            select new CustomerReservationInfo() {
                                Customer = a,
                                Car = c,
                                Reservation = r
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<Contracts.CustomerReservationInfo> GetCustomerOpenReservationInfo(int accountId) {
            using (CarRentalContext entityContext = new CarRentalContext()) {
                var query = from r in entityContext.ReservationSet
                            join a in entityContext.AccountSet on r.AccountId equals a.AccountId
                            join c in entityContext.CarSet on r.CarId equals c.CarId
                            where r.AccountId == accountId
                            select new CustomerReservationInfo() {
                                Customer = a,
                                Car = c,
                                Reservation = r
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}

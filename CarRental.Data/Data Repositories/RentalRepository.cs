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
    [Export(typeof(IRentalRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RentalRepository : DataRepositoryBase<Rental>, IRentalRepository {
        protected override Rental AddEntity(CarRentalContext entityContext, Rental entity) {
            return entityContext.RentalSet.Add(entity);
        }

        protected override Rental UpdateEntity(CarRentalContext entityContext, Rental entity) {
            return (from e in entityContext.RentalSet
                    where e.RentalId == entity.RentalId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Rental> GetEntities(CarRentalContext entityContext) {
            return from e in entityContext.RentalSet
                   select e;
        }

        protected override Rental GetEntity(CarRentalContext entityContext, object id) {
            int idToFind = (int)id;

            var query = from e in entityContext.RentalSet
                        where e.RentalId == idToFind
                        select e;

            var result = query.FirstOrDefault();

            return result;
        }

        public IEnumerable<Rental> GetRentalHistoryByAccount(int accountId) {
            using (CarRentalContext entityContext = new CarRentalContext()) {
                var query = from e in entityContext.RentalSet
                            where e.AccountId == accountId
                            select e;

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<CustomerRentalInfo> GetCurrentCustomerRentalInfo() {
            using (CarRentalContext entityContext = new CarRentalContext()) {
                var query = from r in entityContext.RentalSet
                            where r.DateReturned == null
                            join a in entityContext.AccountSet on r.AccountId equals a.AccountId
                            join c in entityContext.CarSet on r.CarId equals c.CarId
                            select new CustomerRentalInfo() {
                                Customer = a,
                                Car = c,
                                Rental = r
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<Rental> GetRentalHistoryByCar(int carId) {
            using (CarRentalContext entityContext = new CarRentalContext()) {
                var query = from r in entityContext.RentalSet
                            where r.CarId == carId
                            select r;

                return query.ToFullyLoaded();
            }
        }

        public Rental GetCurrentRentalByCar(int carId) {
            using (CarRentalContext entityContext = new CarRentalContext()) {
                var query = from r in entityContext.RentalSet
                            where r.CarId == carId && r.DateReturned == null
                            select r;

                return query.FirstOrDefault();
            }
        }

        public IEnumerable<Rental> GetCurrentlyRentedCars() {
            using (CarRentalContext entityContext = new CarRentalContext()) {
                var query = from r in entityContext.RentalSet
                            where r.DateReturned == null
                            select r;

                return query.ToFullyLoaded();
            }
        }
    }
}

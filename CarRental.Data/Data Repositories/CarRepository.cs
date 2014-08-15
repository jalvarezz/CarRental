using Core.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Business.Entities;
using CarRental.Data.Contracts.Repository_Interfaces;

namespace CarRental.Data {
    [Export(typeof(ICarRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CarRepository : DataRepositoryBase<Car>, ICarRepository {
        protected override Car AddEntity(CarRentalContext entityContext, Car entity) {
            return entityContext.CarSet.Add(entity);
        }

        protected override Car UpdateEntity(CarRentalContext entityContext, Car entity) {
            return (from e in entityContext.CarSet
                    where e.CarId == entity.CarId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Car> GetEntities(CarRentalContext entityContext) {
            return from e in entityContext.CarSet
                   select e;
        }

        protected override Car GetEntity(CarRentalContext entityContext, object id) {
            int idToFind = (int)id;

            var query = from e in entityContext.CarSet
                        where e.CarId == idToFind
                        select e;

            var result = query.FirstOrDefault();

            return result;
        }
    }
}

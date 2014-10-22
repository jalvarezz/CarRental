using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using EntityFramework.Extensions;
using Core.Common.Core;
using StructureMap;
using StructureMap.TypeRules;

namespace CarRental.Data
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IContainer _Container;

        public RepositoryFactory(IContainer Container)
        {
            _Container = Container;
        }

        public IRepository<TEntity> BuildRepository<TEntity>() where TEntity : class, IIdentifiableEntity
        {
            var candidate = _Container.TryGetInstance<IRepository<TEntity>>();

            if (candidate == null)
                throw new NullReferenceException("The requested Repository Type was not registered with the container.");

            return candidate;
        }

        public TRepository BuildCustomRepository<TRepository>() where TRepository : IRepository
        {
            var candidate = _Container.TryGetInstance<TRepository>();

            if (candidate == null)
                throw new NullReferenceException("The requested Repository Type was not registered with the container.");

            return candidate;
        }
    }
}

using CarRental.Data.Contracts;
using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IRepositoryFactory _GenericDataRepositoryFactory;
        private DbContext _Context;

        public UnitOfWork(IRepositoryFactory genericDataRepositoryFactory, DbContext context)
        {
            _GenericDataRepositoryFactory = genericDataRepositoryFactory;
            _Context = context;
        }

        public IRepository<TEntity> BuildRepository<TEntity>() where TEntity : class, IIdentifiableEntity
        {
            return _GenericDataRepositoryFactory.BuildRepository<TEntity>();
        }

        public TRepository BuildCustomRepository<TRepository>() where TRepository : IRepository
        {
            return _GenericDataRepositoryFactory.BuildCustomRepository<TRepository>();
        }

        #region IUnitOfWork Members

        public void Initialize()
        {
        }

        public void Commit()
        {
            try
            {
                _Context.SaveChanges();
            }

            catch (DbEntityValidationException entityValidation)
            {
                foreach (var prop in entityValidation.EntityValidationErrors)
                {
                    Debug.WriteLine(prop);
                }
            }
        }

        public void Rollback()
        {
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _Context.Dispose();
                _Context = null;
            }
        }

        #endregion
    }
}

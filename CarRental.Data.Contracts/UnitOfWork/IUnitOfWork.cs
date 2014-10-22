using Core.Common.Contracts;
using Core.Common.Core;
using System;

namespace CarRental.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> BuildRepository<TEntity>() where TEntity : class, IIdentifiableEntity;
        TRepository BuildCustomRepository<TRepository>() where TRepository : IRepository;

        void Initialize();
        void Commit();
        void Rollback();
    }
}

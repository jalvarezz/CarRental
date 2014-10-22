using Core.Common.Contracts;
using Core.Common.Core;

namespace CarRental.Data.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> BuildRepository<TEntity>() where TEntity : class, IIdentifiableEntity;

        void Initialize();
        void Commit();
        void Rollback();
    }
}

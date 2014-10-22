using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Contracts {
    public interface IRepositoryFactory
    {
        IRepository<TEntity> BuildRepository<TEntity>() where TEntity : class, IIdentifiableEntity;

        TRepository BuildCustomRepository<TRepository>() where TRepository : IRepository;
    }
}

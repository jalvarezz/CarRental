using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Contracts
{
    public interface IRepository<TEntity> where TEntity : class, IIdentifiableEntity
    {
        IEnumerable<TResult> Get<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform,
                                          Expression<Func<TEntity, bool>> filter = null,
                                          Func<IQueryable<TResult>, IQueryable<TResult>> orderby = null,
                                          int? pageSize = null,
                                          int currentPage = 0);

        int GetCount(Expression<Func<TEntity, bool>> filter = null);

        TEntity GetById(object id);

        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        void Update(Expression<Func<TEntity, bool>> filterExpression,
                    Expression<Func<TEntity, TEntity>> updateExpression,
                    out int updatedRecords);

        #region LinqKit Methods

        IEnumerable<TResult> ExpandableGet<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform,
                                                    Expression<Func<TEntity, bool>> filter = null,
                                                    Func<IQueryable<TResult>, IQueryable<TResult>> orderby = null,
                                                    int? pageSize = null,
                                                    int currentPage = 0);

        int ExpandableGetCount(Expression<Func<TEntity, bool>> filter = null);

        #endregion
    }
}

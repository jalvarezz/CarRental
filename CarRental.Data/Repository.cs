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

namespace CarRental.Data
{
    public class Repository<TEntity> 
        : IRepository<TEntity> where TEntity : class, IIdentifiableEntity
    {
        protected internal DbContext _Context;
        protected internal readonly DbSet<TEntity> _DbSet;

        public Repository(DbContext context)
        {
            _Context = context;
            _DbSet = _Context.Set<TEntity>();
        }

        #region IGenericDataRepository Members

        public virtual IEnumerable<TResult> Get<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform,
                                                         Expression<Func<TEntity, bool>> filter = null,
                                                         Func<IQueryable<TResult>, IQueryable<TResult>> orderby = null,
                                                         int? pageSize = null,
                                                         int currentPage = 0)
        {
            var query = filter == null ? this._DbSet : this._DbSet.Where(filter);

            var notSortedResults = transform(query);

            var sortedResults = orderby == null ? notSortedResults : orderby(notSortedResults);

            if (pageSize.HasValue)
            {
                var excludedRows = (currentPage - 1) * pageSize.Value;
                sortedResults = sortedResults.Skip(excludedRows).Take(pageSize.Value);
            }

            return sortedResults.ToList();
        }

        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? this._DbSet.Count() : this._DbSet.Where(filter).Count();
        }

        public virtual TEntity GetById(object id)
        {
            return _DbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _DbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            Delete(GetById(id));
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _DbSet.Attach(entityToDelete);
            }

            _DbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _DbSet.Attach(entityToUpdate);
            _Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Update(Expression<Func<TEntity, bool>> filterExpression, 
                                   Expression<Func<TEntity, TEntity>> updateExpression, 
                                   out int updatedRecords)
        {
            updatedRecords = _DbSet.Where(filterExpression).Update(updateExpression);
        }

        #region LinqKit Methods

        public IEnumerable<TResult> ExpandableGet<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform, 
                                                           Expression<Func<TEntity, bool>> filter = null, 
                                                           Func<IQueryable<TResult>, IQueryable<TResult>> orderby = null, 
                                                           int? pageSize = null, 
                                                           int currentPage = 0)
        {
            var query = filter == null ? this._DbSet.AsExpandable() : this._DbSet.AsExpandable().Where(filter);

            var notSortedResults = transform(query);

            var sortedResults = orderby == null ? notSortedResults : orderby(notSortedResults);

            if (pageSize.HasValue)
            {
                var excludedRows = (currentPage - 1) * pageSize.Value;
                sortedResults = sortedResults.Skip(excludedRows).Take(pageSize.Value);
            }

            return sortedResults.ToList();
        }

        public int ExpandableGetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? this._DbSet.AsExpandable().Count() : this._DbSet.AsExpandable().Where(filter).Count();
        }

        public IQueryable<TEntity> ExpandableGetQuery()
        {
            return this._DbSet.AsExpandable().AsQueryable<TEntity>();
        }

        #endregion

        #endregion
    }
}

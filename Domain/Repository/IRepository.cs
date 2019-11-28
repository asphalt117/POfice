using System;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Repository
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> Get();
        IQueryable<TEntity> GetSkipTake(int skip, int take);
        IQueryable<TEntity> GetBySqlQuery(string sqlquery);
        void SqlQueryToDb(string sqlquery);
        TEntity GetById(int id);
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);
        void Added(TEntity entity);
        void Add(TEntity entity);
        TEntity Insert(TEntity entity);
        void Modify(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteById(int id);
        void Save();
     }
}
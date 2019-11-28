using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Repository
{
    public abstract class CRepository<TEntity> : IRepository<TEntity> 
        where TEntity : class
    {
        protected readonly AbzContext dbcontext;
        protected DbSet<TEntity> dbset;

        public CRepository(AbzContext dbcntxt)
        {
            dbcontext = dbcntxt;
            dbset = dbcontext.Set<TEntity>();
        }

        public CRepository()
        {
            dbcontext = new AbzContext();
            dbset = dbcontext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Get()
        {
            return dbset;
        }

        public virtual IQueryable<TEntity> GetSkipTake(int skip, int take)
        {
            return dbset.Skip(skip).Take(take);
        }

        public virtual IQueryable<TEntity> GetBySqlQuery(string sqlquery)
        {
            return dbset.SqlQuery(sqlquery).AsQueryable<TEntity>();
        }

        public virtual void SqlQueryToDb(string sqlquery)
        {
            dbcontext.Database.ExecuteSqlCommand(sqlquery);
        }

        public virtual TEntity GetById(int id)
        {
            return dbset.Find(id);
        }

        public virtual IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return dbset.Where(predicate);
        }
        
        public virtual int GetCountItems()
        {
            return dbset.Count();
        }

        public virtual void Added(TEntity entity)
        {
            dbcontext.Configuration.AutoDetectChangesEnabled = false;
            dbcontext.Entry(entity).State = EntityState.Added;
            dbcontext.ChangeTracker.DetectChanges();
            dbcontext.SaveChanges();
            dbcontext.Configuration.AutoDetectChangesEnabled = true;
        }

        public virtual void Add(TEntity entity)
        {
            dbcontext.Configuration.AutoDetectChangesEnabled = false;
            dbset.Add(entity);
            dbcontext.ChangeTracker.DetectChanges();
            dbcontext.SaveChanges();
            dbcontext.Configuration.AutoDetectChangesEnabled = true;
        }

        public virtual TEntity Insert(TEntity entity)
        {
            this.Add(entity);
            return entity;
        }

        public virtual void Modify(TEntity entity)
        {
            dbcontext.Configuration.AutoDetectChangesEnabled = false;
            dbcontext.Entry(entity).State = EntityState.Modified;
            dbcontext.ChangeTracker.DetectChanges();
            dbcontext.SaveChanges();
            dbcontext.Configuration.AutoDetectChangesEnabled = true;
        }

        public virtual TEntity Update(TEntity entity)
        {
            this.Modify(entity); 
            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            dbcontext.Configuration.AutoDetectChangesEnabled = false; 
            dbcontext.Entry(entity).State = EntityState.Deleted;
            dbcontext.ChangeTracker.DetectChanges();
            dbcontext.SaveChanges();
            dbcontext.Configuration.AutoDetectChangesEnabled = true;   
        }

        public virtual void DeleteById(int id)
        {
            var entity = this.GetById(id);
            dbcontext.Configuration.AutoDetectChangesEnabled = false; 
            dbset.Remove(entity);
            dbcontext.ChangeTracker.DetectChanges();
            dbcontext.SaveChanges();
            dbcontext.Configuration.AutoDetectChangesEnabled = true;   
        }

        public virtual void Save()
        {
            dbcontext.SaveChanges();
        }
    }
}  
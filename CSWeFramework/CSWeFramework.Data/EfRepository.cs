using CSWeFramework.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSWeFramework.Data
{
    /// <summary>
    /// ef仓储类，跟dbcontext解耦
    /// </summary>
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly IDbContext dbContext;

        private IDbSet<T> dbSet;

        public EfRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<T> Table => this.DbSet;

        protected virtual IDbSet<T> DbSet
        {
            get
            {
                this.dbSet = dbSet ?? this.dbContext.Set<T>();
                return this.dbSet;
            }
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.DbSet.Remove(entity);
            this.dbContext.SaveChanges();
        }

        public void Delete(IEnumerable<T> entitys)
        {
            throw new ArgumentNullException(nameof(entitys));
        }

        public T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            this.DbSet.Add(entity);
            this.dbContext.SaveChanges();
        }

        public void Insert(IEnumerable<T> entitys)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            this.dbContext.SaveChanges();
        }

        public void Update(IEnumerable<T> entitys)
        {
            throw new NotImplementedException();
        }
    }
}

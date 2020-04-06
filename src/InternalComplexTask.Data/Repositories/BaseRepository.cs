using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using InternalComplexTask.Data.Contracts;
using InternalComplexTask.Data.Contracts.Entities;
using InternalComplexTask.Data.Contracts.Models;
using InternalComplexTask.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace InternalComplexTask.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        protected internal readonly DbContext DbContext;
        protected internal readonly DbSet<TEntity> DbSet;

        public BaseRepository(DbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task<IList<TEntity>> GetAllAsync() =>
            await DbSet.ToListAsync();

        public virtual async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate) =>
            await DbSet.Where(predicate).ToListAsync();

        public virtual async Task<PagedResult<TEntity>> GetPagedResultAsync(int page, int pageSize) => 
            await DbSet.GetPagedAsync(page, pageSize);

        public virtual async Task<PagedResult<TEntity>> GetPagedResultAsync(Expression<Func<TEntity, bool>> predicate, int page, int pageSize) =>
            await DbSet.Where(predicate).GetPagedAsync(page, pageSize);

        public virtual async Task<TEntity> GetByIdAsync(int id) =>
            await DbSet.FindAsync(id);

        public virtual void Create(TEntity entity)
        {
            ThrowIfNull(entity);

            DbSet.Add(entity);
        }

        public virtual void Edit(TEntity entity)
        {
            ThrowIfNull(entity);

            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity) =>
            DbSet.Remove(entity);


        private void ThrowIfNull(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }
    }
}

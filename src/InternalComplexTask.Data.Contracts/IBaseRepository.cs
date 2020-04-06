using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using InternalComplexTask.Data.Contracts.Entities;
using InternalComplexTask.Data.Contracts.Models;

namespace InternalComplexTask.Data.Contracts
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<IList<TEntity>> GetAllAsync();
        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
        Task<PagedResult<TEntity>> GetPagedResultAsync(int page, int pageSize);
        Task<PagedResult<TEntity>> GetPagedResultAsync(Expression<Func<TEntity, bool>> predicate, int page, int pageSize);
        Task<TEntity> GetByIdAsync(int id);
        void Create(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TEntity entity);
        
    }
}

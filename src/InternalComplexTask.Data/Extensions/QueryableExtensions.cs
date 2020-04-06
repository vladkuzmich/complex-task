using System;
using System.Linq;
using System.Threading.Tasks;
using InternalComplexTask.Data.Contracts.Entities;
using InternalComplexTask.Data.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace InternalComplexTask.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize)
            where T : BaseEntity
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page, 
                PageSize = pageSize, 
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }
    }
}

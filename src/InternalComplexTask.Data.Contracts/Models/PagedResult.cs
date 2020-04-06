using System.Collections.Generic;
using InternalComplexTask.Data.Contracts.Entities;

namespace InternalComplexTask.Data.Contracts.Models
{
    public class PagedResult<T> : PagedResultBase 
        where T : BaseEntity
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}

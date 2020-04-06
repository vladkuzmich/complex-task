using InternalComplexTask.Data.Contracts;
using InternalComplexTask.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternalComplexTask.Data.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DbContext dbContext) 
            : base(dbContext)
        { }
    }
}

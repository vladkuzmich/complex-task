using InternalComplexTask.Data.Contracts;
using InternalComplexTask.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternalComplexTask.Data.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext dbContext)
            : base(dbContext)
        { }
    }
}

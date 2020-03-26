using InternalComplexTask.Data.Contracts;
using InternalComplexTask.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternalComplexTask.Data.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext dbContext)
            : base(dbContext)
        { }
    }
}

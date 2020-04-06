using InternalComplexTask.Data.Contracts;
using InternalComplexTask.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternalComplexTask.Data.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DbContext dbContext) 
            : base(dbContext)
        { }
    }
}

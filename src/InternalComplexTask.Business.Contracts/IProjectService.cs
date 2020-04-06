using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InternalComplexTask.Business.Contracts.Models.Dtos;

namespace InternalComplexTask.Business.Contracts
{
    public interface IProjectService
    {
        Task<ProjectDto> GetByIdAsync(int id);
        Task<IEnumerable<ProjectDto>> GetAllByCompanyIdAsync(int companyId, int page, int pageSize);
        Task<ProjectDto> CreateAsync(int companyId, ProjectDto projectDto);
        Task EditAsync(int companyId, ProjectDto projectDto);
    }
}

using System.Threading.Tasks;
using InternalComplexTask.Business.Contracts.Models.Dtos;

namespace InternalComplexTask.Business.Contracts
{
    public interface IDepartmentService
    {
        Task<DepartmentDto> GetByIdAsync(int id);
        Task<DepartmentDto> CreateAsync(int companyId, DepartmentDto departmentDto);
    }
}

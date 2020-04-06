using System.Collections.Generic;
using System.Threading.Tasks;
using InternalComplexTask.Business.Contracts.Models.Dtos;

namespace InternalComplexTask.Business.Contracts
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> GetByIdAsync(int id);
        Task<IEnumerable<EmployeeDto>> GetAllByCompanyIdAsync(int companyId, int page, int pageSize);
        Task<IEnumerable<EmployeeDto>> GetAllByProjectIdAsync(int projectId, int page, int pageSize);
        Task<EmployeeDto> CreateAsync(EmployeeDto employeeDto);
        Task EditAsync(int id, EmployeeDto userDto);
        Task DeleteAsync(int id);
        Task DeleteFromProjectAsync(int employeeId, int projectId);
        Task AddOrUpdateImageAsync(int id, string bucketName, byte[] imageData);
        Task AddToProjectAsync(int employeeId, int projectId);
    }
}

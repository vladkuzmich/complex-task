using System.Collections.Generic;
using System.Threading.Tasks;
using InternalComplexTask.Business.Contracts.Models.Dtos;

namespace InternalComplexTask.Business.Contracts
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetByIdAsync(int id);
        Task<IEnumerable<CompanyDto>> GetAllAsync();
    }
}

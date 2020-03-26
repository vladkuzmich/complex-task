using System.Collections.Generic;
using System.Threading.Tasks;
using InternalComplexTask.Business.Contracts;
using InternalComplexTask.Business.Contracts.Models.Dtos;
using InternalComplexTask.Business.Extensions.ModelExtensions;
using InternalComplexTask.Data.Contracts;
using Microsoft.Extensions.Logging;

namespace InternalComplexTask.Business.Services
{
    public class CompanyService : BaseService, ICompanyService
    {
        public CompanyService(IUnitOfWork uow, ILogger<CompanyService> logger)
            : base(uow, logger)
        {
        }

        public async Task<CompanyDto> GetByIdAsync(int id)
        {
            var company = await Uow.Companies.GetByIdAsync(id);

            return company?.ToDto();
        }

        public async Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            var companies = await Uow.Companies.GetAllAsync();

            return companies.ToDtos();
        }
    }
}

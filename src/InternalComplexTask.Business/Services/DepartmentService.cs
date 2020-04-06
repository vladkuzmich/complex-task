using System;
using System.Threading.Tasks;
using InternalComplexTask.Business.Contracts;
using InternalComplexTask.Business.Contracts.Models.Dtos;
using InternalComplexTask.Business.Extensions.ModelExtensions;
using InternalComplexTask.Data.Contracts;
using Microsoft.Extensions.Logging;

namespace InternalComplexTask.Business.Services
{
    public class DepartmentService : BaseService, IDepartmentService
    {
        public DepartmentService(IUnitOfWork uow, ILogger<DepartmentService> logger) 
            : base(uow, logger)
        {
        }

        public async Task<DepartmentDto> GetByIdAsync(int id)
        {
            var department = await Uow.Departments.GetByIdAsync(id);

            return department?.ToDto();
        }

        public async Task<DepartmentDto> CreateAsync(int companyId, DepartmentDto departmentDto)
        {
            var company = await Uow.Companies.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new ArgumentException($"Company with id: '{companyId}' not found", nameof(companyId));
            }

            try
            {
                var entityToCreate = departmentDto.ToEntity();
                entityToCreate.CompanyId = companyId;

                Uow.Departments.Create(entityToCreate);
                await Uow.CommitAsync();

                return entityToCreate.ToDto();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error while creating department");
                throw;
            }
        }
    }
}

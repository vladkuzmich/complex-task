using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InternalComplexTask.Business.Contracts;
using InternalComplexTask.Business.Contracts.Models.Dtos;
using InternalComplexTask.Business.Extensions.ModelExtensions;
using InternalComplexTask.Data.Contracts;
using Microsoft.Extensions.Logging;

namespace InternalComplexTask.Business.Services
{
    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService(IUnitOfWork uow, ILogger<ProjectService> logger) 
            : base(uow, logger)
        {
        }

        public async Task<ProjectDto> GetByIdAsync(int id)
        {
            var project = await Uow.Projects.GetByIdAsync(id);

            return project?.ToDto();
        }

        public async Task<IEnumerable<ProjectDto>> GetAllByCompanyIdAsync(int companyId, int page, int pageSize)
        {
            var company = await Uow.Companies.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new ArgumentException($"Could not found company with id: '{companyId}'", nameof(companyId));
            }

            var projectsPagedResult = await Uow.Projects.GetPagedResultAsync(x => x.CompanyId.Equals(companyId), page, pageSize);

            return projectsPagedResult.Results?.ToDtos();
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByProjectIdAsync(int id)
        {
            var project = await Uow.Projects.GetByIdAsync(id);
            if (project == null)
            {
                throw new ArgumentException($"Could not found project with id: '{id}'", nameof(id));
            }

            var employees = project.EmployeeProjects.Select(x => x.Employee).ToList();

            return employees.Any()
                ? employees.ToDtos()
                : new List<EmployeeDto>();
        }

        public async Task<ProjectDto> CreateAsync(int companyId, ProjectDto projectDto)
        {
            var company = await Uow.Companies.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new ArgumentException($"Company with id: '{companyId}' not found", nameof(companyId));
            }

            try
            {
                var project = projectDto.ToEntity();
                project.CompanyId = companyId;
                
                Uow.Projects.Create(project);
                await Uow.CommitAsync();

                return project.ToDto();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error while creating project");
                throw;
            }
        }

        public async Task EditAsync(int companyId, ProjectDto projectDto)
        {
            var company = await Uow.Companies.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new ArgumentException($"Company with id: '{companyId}' not found", nameof(companyId));
            }

            var project = await Uow.Projects.GetByIdAsync(projectDto.Id);
            if (project == null)
            {
                throw new ArgumentException($"Project with id: '{projectDto.Id}' not found", nameof(projectDto.Id));
            }

            try
            {
                project.Name = projectDto.Name;
                project.Budget = projectDto.Budget;

                Uow.Projects.Edit(project);
                await Uow.CommitAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Error while editing project with id: '{projectDto.Id}'");
                throw;
            }
        }
    }
}

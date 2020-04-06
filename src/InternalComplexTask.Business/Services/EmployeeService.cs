using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InternalComplexTask.Business.Contracts;
using InternalComplexTask.Business.Contracts.Models.Dtos;
using InternalComplexTask.Business.Extensions.ModelExtensions;
using InternalComplexTask.Data.Contracts;
using InternalComplexTask.Data.Contracts.Entities;
using Microsoft.Extensions.Logging;

namespace InternalComplexTask.Business.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IBlobStorageProvider _blobStorageProvider;

        public EmployeeService(
            IUnitOfWork uow,
            ILogger<EmployeeService> logger,
            IBlobStorageProvider blobStorageProvider)
            : base(uow, logger)
        {
            _blobStorageProvider = blobStorageProvider;
        }

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            var employee = await Uow.Employees.GetByIdAsync(id);

            return employee?.ToDto();
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await Uow.Employees.GetAllAsync();

            return employees?.ToDtos();
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllByCompanyIdAsync(int companyId, int page, int pageSize)
        {
            var company = await Uow.Companies.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new ArgumentException($"Company with companyId: '{companyId}' not found", nameof(companyId));
            }

            var employeesPagedResult = await Uow.Employees.GetPagedResultAsync(x => x.CompanyId.Equals(companyId), page, pageSize);
            return employeesPagedResult.Results.ToDtos();
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllByProjectIdAsync(int projectId, int page, int pageSize)
        {
            var project = await Uow.Projects.GetByIdAsync(projectId);
            if (project == null)
            {
                throw new ArgumentException($"Project with companyId: '{projectId}' not found", nameof(projectId));
            }

            var employeesPagedResult = await Uow.Employees.GetPagedResultAsync(x => x.EmployeeProjects.All(ep => ep.ProjectId.Equals(project.Id)), page, pageSize);
            return employeesPagedResult.Results?.ToDtos();
        }

        public async Task<EmployeeDto> CreateAsync(EmployeeDto employeeDto)
        {
            ThrowIfNull(employeeDto);

            var department = await Uow.Departments.GetByIdAsync(employeeDto.DepartmentId);
            if (department == null)
            {
                throw new ArgumentException($"Department with departmentId: '{employeeDto.DepartmentId}' not found", nameof(employeeDto.DepartmentId));
            }

            try
            {
                var employee = employeeDto.ToEntity();
                employee.DepartmentId = department.Id;
                employee.CompanyId = department.CompanyId;

                Uow.Employees.Create(employee);
                await Uow.CommitAsync();

                return employee.ToDto();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error while creating employee");
                throw;
            }
        }

        public async Task EditAsync(int id, EmployeeDto employeeDto)
        {
            ThrowIfNull(employeeDto);

            var employee = await Uow.Employees.GetByIdAsync(employeeDto.Id);
            if (employee == null)
            {
                throw new ArgumentException($"Employee with employeeId: '{id}' not found", nameof(id));
            }

            var department = await Uow.Departments.GetByIdAsync(employeeDto.DepartmentId);
            if (department == null)
            {
                throw new ArgumentException($"Department with departmentId: '{employee.DepartmentId}' not found", nameof(employee.DepartmentId));
            }

            try
            {
                employee.Name = employeeDto.Name;
                employee.Surname = employeeDto.Surname;
                employee.DepartmentId = department.Id;

                Uow.Employees.Edit(employee);
                await Uow.CommitAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Error while editing employee with employeeId: '{employeeDto.Id}'");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await Uow.Employees.GetByIdAsync(id);
            if (employee == null)
            {
                throw new ArgumentException($"Employee with employeeId: '{id}' not found", nameof(id));
            }

            try
            {
                Uow.Employees.Delete(employee);
                await Uow.CommitAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Error while deleting employee with employeeId: '{id}'");
                throw;
            }
        }

        public async Task DeleteFromProjectAsync(int employeeId, int projectId)
        {
            var employee = await Uow.Employees.GetByIdAsync(employeeId);
            if (employee == null)
            {
                throw new ArgumentException($"Employee with employeeId: '{employeeId}' not found", nameof(employeeId));
            }

            var project = await Uow.Projects.GetByIdAsync(projectId);
            if (project == null)
            {
                throw new ArgumentException($"Project with employeeId: '{employeeId}' not found", nameof(employeeId));
            }

            try
            {
                var employeeProject = project.EmployeeProjects.FirstOrDefault(x => x.EmployeeId.Equals(employeeId));
                if (employeeProject != null)
                {
                    project.EmployeeProjects.Remove(employeeProject);
                    await Uow.CommitAsync();
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Error while removing employee from project with EmployeeId: '{employeeId}' and ProjectId: '{projectId}'");
                throw;
            }
        }

        public async Task AddOrUpdateImageAsync(int id, string bucketName, byte[] imageData)
        {
            var employee = await Uow.Employees.GetByIdAsync(id);
            if (employee == null)
            {
                throw new ArgumentException($"Employee with employeeId: '{id}' not found", nameof(id));
            }

            var fileName = string.Empty;

            try
            {
                fileName = $"{id.ToString()}_avatar";

                if (!string.IsNullOrEmpty(employee.ImageUrl))
                {
                    await _blobStorageProvider.TryRemoveFileAsync(bucketName, fileName);
                }
                
                var imageUrl = await _blobStorageProvider.UploadFileAsync(bucketName, fileName, imageData);

                employee.ImageUrl = imageUrl;
                Uow.Employees.Edit(employee);
                await Uow.CommitAsync();
            }
            catch (Exception e)
            {
                await _blobStorageProvider.TryRemoveFileAsync(bucketName, fileName);
                Logger.LogError(e, $"Error while uploading employee's photo with EmployeeId: '{id}'");
                throw;
            }
        }

        public async Task AddToProjectAsync(int employeeId, int projectId)
        {
            var employee = await Uow.Employees.GetByIdAsync(employeeId);
            if (employee == null)
            {
                throw new ArgumentException($"Employee with employeeId: '{employeeId}' not found", nameof(employeeId));
            }

            var project = await Uow.Projects.GetByIdAsync(projectId);
            if (project == null)
            {
                throw new ArgumentException($"Project with project: '{projectId}' not found", nameof(projectId));
            }

            try
            {
                var employeeProject = project.EmployeeProjects.FirstOrDefault(x => x.EmployeeId.Equals(employeeId));
                if (employeeProject == null)
                {
                    project.EmployeeProjects.Add(new EmployeeProject
                    {
                        EmployeeId = employee.Id,
                        ProjectId = project.Id
                    });

                    await Uow.CommitAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void ThrowIfNull(EmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                throw new ArgumentNullException(nameof(employeeDto));
            }
        }
    }
}

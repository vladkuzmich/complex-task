using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using InternalComplexTask.API.Models.Documents;
using InternalComplexTask.API.Models.Extensions;
using InternalComplexTask.API.Models.Settings;
using InternalComplexTask.Business.Contracts;
using Microsoft.Extensions.Options;

namespace InternalComplexTask.API.Controllers
{
    public class EmployeesController : ApiControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly string _imageBucketName;

        public EmployeesController(
            ICompanyService companyService,
            IProjectService projectService,
            IDepartmentService departmentService,
            IEmployeeService employeeService,
            IOptions<Buckets> awsOptions)
            : base(companyService, projectService, departmentService)
        {
            _employeeService = employeeService;
            _imageBucketName = awsOptions.Value.Images;
        }

        #region GET

        // GET: api/employees/5
        [HttpGet("api/employees/{id}")]
        public async Task<ActionResult<EmployeeDocument>> GetById(int id)
        {
            var employeeDto = await _employeeService.GetByIdAsync(id);
            if (employeeDto == null)
            {
                return NotFound($"Employee with id: '{id}' not found");
            }

            return employeeDto.ToEmployeeDocument();
        }

        // GET: api/companies/2/employees
        [HttpGet("api/companies/{companyId}/employees")]
        public async Task<ActionResult<EmployeeDocument>> GetAllByCompanyId(int companyId, int page = 1)
        {
            var company = await CompanyService.GetByIdAsync(companyId);
            if (company == null)
            {
                return NotFound($"Company with id: '{companyId}' not found");
            }

            var employeeDtos = await _employeeService.GetAllByCompanyIdAsync(companyId, page, 10);
            return Ok(employeeDtos.ToEmployeeDocuments());
        }

        // GET: api/projects/2/employees
        [HttpGet("api/projects/{projectId}/employees")]
        public async Task<ActionResult<EmployeeDocument>> GetAllByProjectId(int projectId, int page = 1)
        {
            var projectDto = await ProjectService.GetByIdAsync(projectId);
            if (projectDto == null)
            {
                return NotFound($"Project with id: '{projectId}' not found");
            }

            var employeeDtos = await _employeeService.GetAllByProjectIdAsync(projectId, page, 10);
            return Ok(employeeDtos.ToEmployeeDocuments());
        }

        // GET: api/employees/2/image
        [HttpGet("api/employees/{id}/image")]
        public async Task<ActionResult<string>> GetImage(int id)
        {
            var employeeDto = await _employeeService.GetByIdAsync(id);
            if (employeeDto == null)
            {
                return NotFound($"Employee with id: '{id}' not found");
            }

            return Ok(employeeDto.ImageUrl);
        }

        #endregion

        #region POST

        // POST: api/employees
        [HttpPost("api/employees")]
        public async Task<ActionResult<EmployeeDocument>> Create([FromBody]EmployeeDocument employee)
        {
            var departmentDto = await DepartmentService.GetByIdAsync(employee.DepartmentId);
            if (departmentDto == null)
            {
                return NotFound($"Department with id: '{employee.DepartmentId}' not found");
            }

            var createEmployee = await _employeeService.CreateAsync(employee.ToEmployeeDto());

            return CreatedAtAction(nameof(GetById), new { id = createEmployee.Id }, createEmployee.ToEmployeeDocument());
        }

        //POST: api/employees/5/upload
        [HttpPost("api/employees/{id}/upload")]
        public async Task<ActionResult> Upload(int id, IFormFile file)
        {
            var employeeDto = await _employeeService.GetByIdAsync(id);
            if (employeeDto == null)
            {
                return NotFound($"User with id: '{id}' not found");
            }

            if (file == null)
            {
                return BadRequest();
            }

            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            await _employeeService.AddOrUpdateImageAsync(id, _imageBucketName, memoryStream.ToArray());

            return Ok();
        }

        // POST: api/projects/3/employees
        [HttpPost("api/projects/{projectId}/employees/{employeeId}")]
        public async Task<ActionResult> AddEmployeeToProject(int projectId, int employeeId)
        {
            var projectDto = await ProjectService.GetByIdAsync(projectId);
            if (projectDto == null)
            {
                return NotFound($"Project with id: '{projectId}' not found");
            }

            var employeeDto = await _employeeService.GetByIdAsync(employeeId);
            if (employeeDto == null)
            {
                return NotFound($"Employee with id: '{employeeId}' not found");
            }

            await _employeeService.AddToProjectAsync(employeeId, projectId);

            return Ok();
        }

        #endregion

        #region PUT

        //PUT: api/employees/1
        [HttpPut("api/employees/{id}")]
        public async Task<ActionResult<EmployeeDocument>> Edit(int id, [FromBody]EmployeeDocument employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            var employeeDto = await _employeeService.GetByIdAsync(id);
            if (employeeDto == null)
            {
                return NotFound($"Employee with id: '{id}' not found");
            }

            var departmentDto = await DepartmentService.GetByIdAsync(employee.DepartmentId);
            if (departmentDto == null)
            {
                return NotFound($"Department with id: '{employee.DepartmentId}' not found");
            }

            await _employeeService.EditAsync(id, employee.ToEmployeeDto());

            return NoContent();
        }

        #endregion

        #region DELETE

        // DELETE: api/projects/2/employee/2
        [HttpDelete("api/projects/{projectId}/employees/{employeeId}")]
        public async Task<ActionResult> DeleteEmployeeFromProject(int projectId, int employeeId)
        {
            var projectDto = await ProjectService.GetByIdAsync(projectId);
            if (projectDto == null)
            {
                return NotFound($"Project with id: '{projectId}' not found");
            }

            var employeeDto = await _employeeService.GetByIdAsync(employeeId);
            if (employeeDto == null)
            {
                return NotFound($"Project with id: '{employeeId}' not found");
            }

            await _employeeService.DeleteFromProjectAsync(employeeId, projectId);

            return Ok();
        }

        // DELETE: api/employees/5
        [HttpDelete("api/employees/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var employeeDto = await _employeeService.GetByIdAsync(id);

            if (employeeDto == null)
            {
                return NotFound($"Employee with id: '{id}' not found");
            }

            await _employeeService.DeleteAsync(id);

            return NoContent();
        }

        #endregion
    }
}

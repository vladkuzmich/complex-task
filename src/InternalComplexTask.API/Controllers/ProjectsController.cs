using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InternalComplexTask.API.Models.Documents;
using InternalComplexTask.API.Models.Extensions;
using InternalComplexTask.Business.Contracts;

namespace InternalComplexTask.API.Controllers
{
    public class ProjectsController : ApiControllerBase
    {
        public ProjectsController(
            ICompanyService companyService,
            IProjectService projectService,
            IDepartmentService departmentService)
            : base(companyService, projectService, departmentService)
        {
        }

        #region GET

        // GET: api/projects/2
        [HttpGet("api/projects/{id}")]
        public async Task<ActionResult<ProjectDocument>> GetById(int id)
        {
            var projectDto = await ProjectService.GetByIdAsync(id);

            if (projectDto == null)
            {
                return NotFound($"Project with id: '{id}' not found");
            }

            return projectDto.ToDocument();
        }

        // GET: api/companies/2/projects
        [HttpGet("api/companies/{companyId}/projects")]
        public async Task<ActionResult<IEnumerable<ProjectDocument>>> GetAllByCompanyId(int companyId, int page = 1)
        {
            var сompanyDto = await CompanyService.GetByIdAsync(companyId);
            if (сompanyDto == null)
            {
                return NotFound($"Company with id: '{companyId}' not found");
            }

            var projectDtos = await ProjectService.GetAllByCompanyIdAsync(companyId, page, 10);
            return Ok(projectDtos.ToDocuments());
        }

        #endregion

        #region POST

        // POST: api/companies/2/projects
        [HttpPost("api/companies/{companyId}/projects")]
        public async Task<ActionResult> Create(int companyId, [FromBody]ProjectDocument project)
        {
            var companyDto = await CompanyService.GetByIdAsync(companyId);
            if (companyDto == null)
            {
                return NotFound($"Company with id: '{companyId}' not found");
            }

            var createdProject = await ProjectService.CreateAsync(companyId, project.ToDto());

            return CreatedAtAction(nameof(GetById), new { id = createdProject.Id }, createdProject.ToDocument());
        }

        //PUT: api/companies/2/projects/2
        [HttpPut("api/companies/{companyId}/projects/{projectId}")]
        public async Task<ActionResult<EmployeeDocument>> Edit(int companyId, int projectId, [FromBody]ProjectDocument project)
        {
            if (projectId != project.Id)
            {
                return BadRequest();
            }

            var employeeDto = await CompanyService.GetByIdAsync(companyId);
            if (employeeDto == null)
            {
                return NotFound($"Employee with id: '{companyId}' not found");
            }

            var projectDto = await ProjectService.GetByIdAsync(projectId);
            if (projectDto == null)
            {
                return NotFound($"Project with id: '{projectId}' not found");
            }

            await ProjectService.EditAsync(companyId, project.ToDto());

            return NoContent();
        }

        #endregion
    }
}

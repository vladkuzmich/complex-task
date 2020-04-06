using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InternalComplexTask.API.Models.Documents;
using InternalComplexTask.API.Models.Extensions;
using InternalComplexTask.Business.Contracts;

namespace InternalComplexTask.API.Controllers
{
    public class DepartmentsController : ApiControllerBase
    {
        public DepartmentsController(
            ICompanyService companyService,
            IProjectService projectService,
            IDepartmentService departmentService)
            : base(companyService, projectService, departmentService)
        {
        }

        #region GET

        // GET: api/departments/5
        [HttpGet("api/departments/{id}")]
        public async Task<ActionResult<DepartmentDocument>> GetById(int id)
        {
            var departmentDto = await DepartmentService.GetByIdAsync(id);

            if (departmentDto == null)
            {
                return NotFound($"Department with id: '{id}' not found");
            }

            return departmentDto.ToDocument();
        }

        #endregion

        #region POST

        // POST: api/companies/{companyId}/departments
        [HttpPost("api/companies/{companyId}/departments")]
        public async Task<ActionResult<DepartmentDocument>> Create(int companyId, [FromBody]DepartmentDocument department)
        {
            var companyDto = await CompanyService.GetByIdAsync(companyId);
            if (companyDto == null)
            {
                return NotFound($"Company with id: '{companyId}' not found");
            }

            var createdDepartment = await DepartmentService.CreateAsync(companyId, department.ToDto());

            return CreatedAtAction(nameof(GetById), new { id = createdDepartment.Id }, createdDepartment.ToDocument());
        }

        #endregion
    }
}

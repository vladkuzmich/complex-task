using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InternalComplexTask.API.Models.Documents;
using InternalComplexTask.API.Models.Extensions;
using InternalComplexTask.Business.Contracts;

namespace InternalComplexTask.API.Controllers
{
    [Route("api/[controller]")]
    public class CompaniesController : ApiControllerBase
    {
        public CompaniesController(
            ICompanyService companyService, 
            IProjectService projectService, 
            IDepartmentService departmentService) 
            : base(companyService, projectService, departmentService)
        {
        }

        #region GET

        // GET: api/companies
        [HttpGet]
        public async Task<IEnumerable<CompanyDocument>> GetAll()
        {
            var companies = await CompanyService.GetAllAsync();

            return companies.ToCompanyDocuments();
        }

        // GET: api/companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDocument>> GetById(int id)
        {
            var companyDto = await CompanyService.GetByIdAsync(id);

            if (companyDto == null)
            {
                return NotFound();
            }

            return companyDto.ToCompanyDocument();
        }

        #endregion
    }
}

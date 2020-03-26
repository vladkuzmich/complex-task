using InternalComplexTask.Business.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace InternalComplexTask.API.Controllers
{
    [ApiController] 
    public class ApiControllerBase : ControllerBase
    {
        protected readonly ICompanyService CompanyService;
        protected readonly IProjectService ProjectService;
        protected readonly IDepartmentService DepartmentService;

        public ApiControllerBase(
            ICompanyService companyService,
            IProjectService projectService,
            IDepartmentService departmentService)
        {
            CompanyService = companyService;
            ProjectService = projectService;
            DepartmentService = departmentService;
        }
    }
}

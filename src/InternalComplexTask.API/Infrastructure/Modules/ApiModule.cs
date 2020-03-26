using Autofac;
using FluentValidation;
using InternalComplexTask.API.Models.Documents;

namespace InternalComplexTask.API.Infrastructure.Modules
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<EmployeeDocumentValidator>()
                .As<IValidator<EmployeeDocument>>();

            builder
                .RegisterType<CompanyDocumentValidator>()
                .As<IValidator<CompanyDocument>>();

            builder
                .RegisterType<DepartmentDocumentValidator>()
                .As<IValidator<DepartmentDocument>>();

            builder
                .RegisterType<ProjectDocumentValidator>()
                .As<IValidator<ProjectDocument>>();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Autofac;
using InternalComplexTask.Data;
using InternalComplexTask.Data.Contracts;
using InternalComplexTask.Data.Repositories;

namespace InternalComplexTask.API.Infrastructure.Modules
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ApplicationContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<EmployeeRepository>()
                .As<IEmployeeRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<CompanyRepository>()
                .As<ICompanyRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ProjectRepository>()
                .As<IProjectRepository>()
                .InstancePerLifetimeScope();
        }
    }
}

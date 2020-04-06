using Microsoft.Extensions.Configuration;
using Autofac;
using Minio;
using InternalComplexTask.Business.Contracts;
using InternalComplexTask.Business.Providers;
using InternalComplexTask.Business.Services;

namespace InternalComplexTask.API.Infrastructure.Modules
{
    public class BusinessModule : Module
    {
        private readonly IConfiguration _configuration;

        public BusinessModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<EmployeeService>()
                .As<IEmployeeService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<CompanyService>()
                .As<ICompanyService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ProjectService>()
                .As<IProjectService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<DepartmentService>()
                .As<IDepartmentService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<AwsBlobStorageProvider>()
                .As<IBlobStorageProvider>()
                .InstancePerLifetimeScope();

            var accessKey = _configuration.GetValue<string>("Aws:AccessKeyID");
            var secretKey = _configuration.GetValue<string>("Aws:SecretAccessKey");
            var storageBaseUrl = _configuration.GetValue<string>("Aws:StorageBaseUrl");

            builder.Register(c => new MinioClient(
                endpoint: storageBaseUrl,
                accessKey: accessKey,
                secretKey: secretKey).WithSSL());
        }
    }
}

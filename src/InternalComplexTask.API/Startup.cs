using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Autofac;
using FluentValidation.AspNetCore;
using InternalComplexTask.API.Infrastructure.Middleware;
using InternalComplexTask.API.Infrastructure.Modules;
using InternalComplexTask.API.Models;
using InternalComplexTask.API.Models.Settings;
using InternalComplexTask.Data;

namespace InternalComplexTask.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Buckets>(Configuration.GetSection("Aws:Buckets"));

            services.AddDbContext<ApplicationContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));

            services.AddCors();

            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API"
                });
            });

            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .AddFluentValidation();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new BusinessModule(Configuration));
            builder.RegisterModule(new DataModule());
            builder.RegisterModule(new ApiModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var origins = Configuration
                .GetSection("Cors")
                .Get<CorsSettings>()
                .Rules
                .Where(x => x.Allow)
                .Select(x => x.Origin)
                .ToArray();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder.WithOrigins(origins));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "InternalComplexTask API");
            });
        }
    }
}

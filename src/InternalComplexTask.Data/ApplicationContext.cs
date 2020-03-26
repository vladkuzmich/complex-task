using Microsoft.EntityFrameworkCore;
using InternalComplexTask.Data.Contracts.Entities;
using InternalComplexTask.Data.EntityConfigurations;

namespace InternalComplexTask.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeProjectConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

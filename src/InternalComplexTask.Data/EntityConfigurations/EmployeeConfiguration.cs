using InternalComplexTask.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternalComplexTask.Data.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .HasKey(employee => employee.Id);

            builder
                .Property(employee => employee.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(employee => employee.Surname)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(employee => employee.Gender)
                .HasConversion<string>();

            builder
                .Property(employee => employee.ImageUrl)
                .IsRequired(false);

            builder
                .HasOne(employee => employee.Company)
                .WithMany(company => company.Employees)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(default);

            builder
                .HasOne(user => user.Department)
                .WithMany(department => department.Employees)
                .HasForeignKey(x => x.DepartmentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

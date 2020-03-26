using InternalComplexTask.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternalComplexTask.Data.EntityConfigurations
{
    public class EmployeeProjectConfiguration : IEntityTypeConfiguration<EmployeeProject>
    {
        public void Configure(EntityTypeBuilder<EmployeeProject> builder)
        {
            builder
                .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

            builder
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeeProjects)
                .HasForeignKey(ep => ep.EmployeeId);

            builder
                .HasOne(ep => ep.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectId);
        }
    }
}

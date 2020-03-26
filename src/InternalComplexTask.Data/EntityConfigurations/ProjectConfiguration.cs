using InternalComplexTask.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternalComplexTask.Data.EntityConfigurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
                .HasKey(project => project.Id);

            builder
                .Property(project => project.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(project => project.Budget)
                .HasColumnType("money");

            builder
                .HasOne(project => project.Company)
                .WithMany(company => company.Projects)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(default);
        }
    }
}

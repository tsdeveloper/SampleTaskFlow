using System.Diagnostics.CodeAnalysis;
using Core.Entities;
using Infra.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Config
{
        [ExcludeFromCodeCoverage]
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> b)
        {
            b.HasKey(x => x.ProjectId);

            b.Property(x => x.ProjectUserId)
                .IsRequired();

            b.Property(x => x.ProjectName)
                .HasMaxLength(100)
                .IsRequired();

            b.Property(x => x.ProjectCompletedAt)
                .IsRequired(false);

            b.Property(x => x.ProjectMaxLimitTask)
                .IsRequired();

            b.Property(x => x.ProjectStatus)
                .HasDefaultValue(EProjectStatusType.NOSTARTING)
                .HasConversion<ProjectStatusTypeConverter>()
                .IsRequired();

            b.Property(x => x.CreatedAt)
                .HasDefaultValue(DateTime.UtcNow)
                .IsRequired();    

            b.Property(x => x.UpdatedAt)
                .IsRequired(false);            
        }
    }
}

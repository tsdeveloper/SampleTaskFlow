using Core.Entities;
using Infra.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Config
{
    public class TaskConfig : IEntityTypeConfiguration<Core.Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Task> b)
        {
            b.HasKey(x => x.TaskId);

            b.Property(x => x.TaskName)
                .HasDefaultValue(100)
                .IsRequired();

            b.Property(x => x.TaskDescription)
                .HasDefaultValue(200)
                .IsRequired();

            b.Property(x => x.TaskPriority)
                .HasDefaultValue(ETaskPriorityType.LOW)
                .HasConversion<TaskPriorityTypeConverter>()
                .IsRequired();

            b.Property(x => x.TaskStatus)
                .HasDefaultValue(ETaskStatusType.NOSTARTING)
                .HasConversion<TaskStatusTypeConverter>()
                .IsRequired();

            b.HasOne(x => x.Project)
                .WithMany(d => d.TaskList)
                .HasForeignKey(x => x.ProjectId);

            b.Property(x => x.CreatedAt)
                .HasDefaultValue(DateTime.UtcNow)
                .IsRequired();

            b.Property(x => x.UpdatedAt)
                .IsRequired(false);

        }
    }
}

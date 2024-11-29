using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Config
{
    public class TaskCommentConfig : IEntityTypeConfiguration<TaskComment>
    {
        public void Configure(EntityTypeBuilder<TaskComment> b)
        {
            b.HasKey(x => x.TaskCommentId);

            b.Property(x => x.TaskCommentDescription)
                .HasMaxLength(200)
                .IsRequired();

            b.HasOne(x => x.Task)
                .WithMany(x => x.TaskCommentList)
                .HasForeignKey(x => x.TaskId); 

            b.Property(x => x.CreatedAt)
                .HasDefaultValue(DateTime.UtcNow)
                .IsRequired();    

            b.Property(x => x.UpdatedAt)
                .IsRequired(false);                                 
        }
    }
}

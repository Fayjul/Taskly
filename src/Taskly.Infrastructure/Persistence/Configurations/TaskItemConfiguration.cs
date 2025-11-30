using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly.Domain.Entities;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Assignee)
               .WithMany()
               .HasForeignKey(t => t.AssigneeId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(t => t.Creator)
               .WithMany()
               .HasForeignKey(t => t.CreatedBy)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Project)
               .WithMany(p => p.Tasks)
               .HasForeignKey(t => t.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

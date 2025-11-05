using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly.Domain.Entities;

public class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMember>
{
    public void Configure(EntityTypeBuilder<ProjectMember> builder)
    {
        builder.HasKey(pm => new { pm.ProjectId, pm.UserId });

        builder.HasOne(pm => pm.Project)
               .WithMany(p => p.Members)
               .HasForeignKey(pm => pm.ProjectId);

        builder.HasOne(pm => pm.User)
               .WithMany(u => u.ProjectMembers)
               .HasForeignKey(pm => pm.UserId);
    }
}

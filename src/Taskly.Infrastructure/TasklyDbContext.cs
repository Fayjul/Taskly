using Microsoft.EntityFrameworkCore;
using Taskly.Domain.Entities;

public class TasklyDbContext : DbContext
{
    public TasklyDbContext(DbContextOptions<TasklyDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectMember> ProjectMembers => Set<ProjectMember>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TasklyDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
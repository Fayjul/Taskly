namespace Taskly.Domain.Entities
{
    public class ProjectMember
    {
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; } = "Member";
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Project Project { get; set; } = default!;
        public User User { get; set; } = default!;
    }
}

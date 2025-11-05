namespace Taskly.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public string Status { get; set; } = "Pending"; // e.g. Pending, InProgress, Completed
        public Guid? AssigneeId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Project Project { get; set; } = default!;
        public User? Assignee { get; set; }
        public User Creator { get; set; } = default!;
    }
}

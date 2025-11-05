namespace Taskly.Domain.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string Type { get; set; } = default!;  // e.g., TaskAssigned, TaskUpdated
        public string? Payload { get; set; }          // JSON details of event
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public User? User { get; set; }
    }
}

namespace Taskly.Domain.Entities
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Project Project { get; set; } = default!;
        public User User { get; set; } = default!;
    }
}

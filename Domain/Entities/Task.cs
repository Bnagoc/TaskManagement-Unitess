namespace Domain.Entities
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.ToDo;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public User CreatedBy { get; set; }
        public Guid CreatedById { get; set; }
        public virtual List<User> Users { get; set; }
    }
}

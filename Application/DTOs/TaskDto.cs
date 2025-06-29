namespace Application.DTOs
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public UserDto CreatedBy { get; set; }
        public List<UserDto> Users { get; set; } = [];
    }
}

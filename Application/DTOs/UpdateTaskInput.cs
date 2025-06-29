namespace Application.DTOs
{
    public class UpdateTaskInput
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Domain.Enums.TaskStatus? Status { get; set; }
    }
}

using Application.DTOs;

namespace Application.Mappers
{
    public class TaskMapper
    {
        public TaskDto CreateTaskDto(Domain.Entities.Task task)
        {
            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                CreatedAt = task.CreatedAt,
                CreatedById = task.CreatedById,
                Users = task.Users.Any() ? task.Users.Select(x => new UserDto
                {
                    Id = x.Id,
                    Username = x.Username,
                    Email = x.Email.Value,
                    Role = x.Role.ToString()
                }).ToList() : []
            };
        }
    }
}

using Application.DTOs;

namespace Application.Interfaces
{
    public interface ITaskService
    {
        IQueryable<Domain.Entities.Task> GetTasksQuery();
        Task<TaskDto> GetTaskByIdAsync(Guid id);
        Task<TaskDto> CreateTaskAsync(CreateTaskInput input, Guid createdById);
        Task<TaskDto> AssignTaskToUserAsync(Guid taskId, Guid userId);
        Task<TaskDto> UpdateTaskAsync(UpdateTaskInput input);
        Task<bool> DeleteTaskAsync(Guid id);
    }
}

using Application.Commands.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.Queries.Tasks;

namespace Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly GetTasksQuery _getTasksQuery;
        private readonly GetTaskByIdQuery _getTaskByIdQuery;
        private readonly CreateTaskCommand _createTaskCommand;
        private readonly UpdateTaskCommand _updateTaskCommand;
        private readonly DeleteTaskCommand _deleteTaskCommand;
        private readonly AssignTaskToUserCommand _assignTaskToUserCommand;

        public TaskService(
            GetTasksQuery getTasksQuery, GetTaskByIdQuery getTaskByIdQuery, 
            CreateTaskCommand createTaskCommand, UpdateTaskCommand updateTaskCommand, 
            DeleteTaskCommand deleteTaskCommand, AssignTaskToUserCommand assignTaskToUserCommand
            )
        {
            _getTasksQuery = getTasksQuery;
            _getTaskByIdQuery = getTaskByIdQuery;
            _createTaskCommand = createTaskCommand;
            _updateTaskCommand = updateTaskCommand;
            _deleteTaskCommand = deleteTaskCommand;
            _assignTaskToUserCommand = assignTaskToUserCommand;
        }

        public IQueryable<Domain.Entities.Task> GetTasksQuery()
        {
            return _getTasksQuery.Execute();
        }

        public async Task<TaskDto> GetTaskByIdAsync(Guid id)
        {
            return await _getTaskByIdQuery.ExecuteAsync(id);
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskInput input, Guid createdById)
        {
            return await _createTaskCommand.ExecuteAsync(input, createdById);
        }

        public async Task<TaskDto> AssignTaskToUserAsync(Guid taskId, Guid userId)
        {
            return await _assignTaskToUserCommand.ExecuteAsync(taskId, userId);
        }

        public async Task<TaskDto> UpdateTaskAsync(UpdateTaskInput input)
        {
            return await _updateTaskCommand.ExecuteAsync(input);
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            return await _deleteTaskCommand.ExecuteAsync(id);
        }

    }
}

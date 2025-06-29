using Application.DTOs;
using Application.Mappers;
using Domain.Interfaces;

namespace Application.Commands.Tasks
{
    public class CreateTaskCommand
    {
        private readonly TaskMapper _taskMapper;
        private readonly ITaskRepository _taskRepository;

        public CreateTaskCommand(TaskMapper taskMapper, ITaskRepository taskRepository)
        {
            _taskMapper = taskMapper;
            _taskRepository = taskRepository;
        }

        public async Task<TaskDto> ExecuteAsync(CreateTaskInput input, Guid createdById)
        {
            var task = new Domain.Entities.Task
            {
                Title = input.Title,
                Description = input.Description,
                CreatedById = createdById
            };

            await _taskRepository.AddAsync(task);

            return _taskMapper.CreateTaskDto(task);
        }
    }
}

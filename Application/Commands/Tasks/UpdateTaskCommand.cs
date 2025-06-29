using Application.DTOs;
using Application.Mappers;
using Domain.Interfaces;
using SharedKernel.Exceptions;

namespace Application.Commands.Tasks
{
    public class UpdateTaskCommand
    {
        private readonly TaskMapper _taskMapper;
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskCommand(TaskMapper taskMapper, ITaskRepository taskRepository)
        {
            _taskMapper = taskMapper;
            _taskRepository = taskRepository;
        }

        public async Task<TaskDto> ExecuteAsync(UpdateTaskInput input)
        {
            var task = await _taskRepository.GetByIdAsync(input.Id);
            if (task == null) throw new NotFoundException("Task not found");

            task.Title = input.Title ?? task.Title;
            task.Description = input.Description ?? task.Description;
            task.Status = input.Status ?? task.Status;

            await _taskRepository.UpdateAsync(task);

            return _taskMapper.CreateTaskDto(task);
        }
    }
}

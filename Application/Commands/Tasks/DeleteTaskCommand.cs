using Application.Mappers;
using Domain.Interfaces;
using SharedKernel.Exceptions;

namespace Application.Commands.Tasks
{
    public class DeleteTaskCommand
    {
        private readonly TaskMapper _taskMapper;
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskCommand(TaskMapper taskMapper, ITaskRepository taskRepository)
        {
            _taskMapper = taskMapper;
            _taskRepository = taskRepository;
        }

        public async Task<bool> ExecuteAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) throw new NotFoundException("Task not found");

            await _taskRepository.DeleteAsync(id);
            return true;
        }
    }
}

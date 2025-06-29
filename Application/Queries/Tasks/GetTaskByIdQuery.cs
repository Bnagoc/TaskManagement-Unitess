using Application.DTOs;
using Application.Mappers;
using Domain.Interfaces;
using SharedKernel.Exceptions;

namespace Application.Queries.Tasks
{
    public class GetTaskByIdQuery
    {
        private readonly TaskMapper _taskMapper;
        private readonly ITaskRepository _taskRepository;

        public GetTaskByIdQuery(TaskMapper taskMapper, ITaskRepository taskRepository)
        {
            _taskMapper = taskMapper;
            _taskRepository = taskRepository;
        }

        public async Task<TaskDto> ExecuteAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                throw new NotFoundException("Task not found");
            }

            return _taskMapper.CreateTaskDto(task);
        }
    }
}

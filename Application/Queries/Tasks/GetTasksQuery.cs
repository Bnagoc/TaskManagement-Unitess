using Application.DTOs;
using Application.Mappers;
using Domain.Interfaces;

namespace Application.Queries.Tasks
{
    public class GetTasksQuery
    {
        private readonly TaskMapper _taskMapper;
        private readonly ITaskRepository _taskRepository;

        public GetTasksQuery(ITaskRepository taskRepository, TaskMapper taskMapper)
        {
            _taskRepository = taskRepository;
            _taskMapper = taskMapper;
        }

        public IQueryable<TaskDto> Execute()
        {
            var tasks = _taskRepository.GetTasksQuery();

            return tasks?.Select(task => _taskMapper.CreateTaskDto(task));
        }
    }
}

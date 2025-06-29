using Application.DTOs;
using Application.Mappers;
using Domain.Interfaces;
using SharedKernel.Exceptions;

namespace Application.Commands.Tasks
{
    public class AssignTaskToUserCommand
    {
        private readonly TaskMapper _taskMapper;
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;

        public AssignTaskToUserCommand(TaskMapper taskMapper, ITaskRepository taskRepository, IUserRepository userRepository)
        {
            _taskMapper = taskMapper;
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        public async Task<TaskDto> ExecuteAsync(Guid taskId, Guid userId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null) throw new NotFoundException("Task not found");

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) throw new NotFoundException("User not found");

            task.Users.Add(user);
            await _taskRepository.UpdateAsync(task);

            return _taskMapper.CreateTaskDto(task);
        }
    }
}

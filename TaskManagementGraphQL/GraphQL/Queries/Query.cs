using Application.DTOs;
using Application.Interfaces;
using HotChocolate.Authorization;

namespace Presentation.GraphQL.Queries
{
    public class Query
    {
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Domain.Entities.Task> GetTasks([Service] ITaskService taskService)
        {
            return taskService.GetTasksQuery();
        }

        public async Task<TaskDto> GetTaskById(Guid id, [Service] ITaskService taskService)
        {
            return await taskService.GetTaskByIdAsync(id);
        }

        [Authorize(Roles = new[] { "Admin" })]
        [UsePaging]
        [UseSorting]
        public IQueryable<UserDto> GetUsers([Service] IUserService userService)
        {
            return userService.GetUsersQuery();
        }
    }
}

using System.Security.Claims;
using Application.DTOs;
using Application.Interfaces;
using HotChocolate.Authorization;
using Infrastructure.Data.Authentication;
using SharedKernel.Constants;

namespace Presentation.GraphQL.Mutations
{
    public class Mutation
    {
        [AllowAnonymous]
        public async Task<string> Login(
            LoginUserInput input,
            [Service] IUserService userService,
            [Service] IJwtTokenService jwtTokenService)
        {
            return await userService.LoginUserAsync(input);
        }

        [AllowAnonymous]
        public async Task<UserDto> Register(
            RegisterUserInput input,
            [Service] IUserService userService)
        {
            return await userService.RegisterUserAsync(input);
        }

        [Authorize]
        public async Task<TaskDto> CreateTask(
            CreateTaskInput input,
            [Service] ITaskService taskService,
            ClaimsPrincipal user)
        {
            var userId = Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
            return await taskService.CreateTaskAsync(input, userId);
        }

        [Authorize]
        public async Task<TaskDto> UpdateTask(
            UpdateTaskInput input,
            [Service] ITaskService taskService,
            ClaimsPrincipal user)
        {
            return await taskService.UpdateTaskAsync(input);
        }

        [Authorize]
        public async Task<bool> DeleteTask(
            Guid input,
            [Service] ITaskService taskService,
            ClaimsPrincipal user)
        {
            return await taskService.DeleteTaskAsync(input);
        }

        [Authorize(Roles = new[] { "Admin" })]
        public async Task<TaskDto> AssignTask(
            AssignTaskInput input,
            [Service] ITaskService taskService)
        {
            return await taskService.AssignTaskToUserAsync(input.TaskId, input.UserId);
        }


    }
}

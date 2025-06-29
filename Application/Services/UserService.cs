using Application.Commands.Auth;
using Application.DTOs;
using Application.Interfaces;
using Application.Queries.Users;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly RegisterUserCommand _registerUserCommand;
        private readonly LoginUserCommand _loginUserCommand;
        private readonly GetUsersQuery _getUsersQuery;

        public UserService(
            RegisterUserCommand registerUserCommand, LoginUserCommand loginUserCommand, 
            GetUsersQuery getUsersQuery
            )
        {
            _registerUserCommand = registerUserCommand;
            _loginUserCommand = loginUserCommand;
            _getUsersQuery = getUsersQuery;
        }

        public IQueryable<UserDto> GetUsersQuery()
        {
            return _getUsersQuery.Execute();
        }

        public async Task<UserDto> RegisterUserAsync(RegisterUserInput input)
        {
            return await _registerUserCommand.ExecuteAsync(input);
        }

        public async Task<string> LoginUserAsync(LoginUserInput input)
        {
            return await _loginUserCommand.ExecuteAsync(input);
        }
    }
}

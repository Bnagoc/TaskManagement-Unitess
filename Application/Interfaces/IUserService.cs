using Application.DTOs;

namespace Application.Interfaces
{
    public interface IUserService
    {
        IQueryable<UserDto> GetUsersQuery();
        Task<UserDto> RegisterUserAsync(RegisterUserInput input);
        Task<string> LoginUserAsync(LoginUserInput input);
    }
}

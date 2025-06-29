using Application.DTOs;
using Domain.Interfaces;

namespace Application.Queries.Users
{
    public class GetUsersQuery
    {
        private readonly IUserRepository _userRepository;

        public GetUsersQuery(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IQueryable<UserDto> Execute()
        {
            var users = _userRepository.GetUsersQuery();

            return users.Select(x => new UserDto
            {
                Id = x.Id,
                Username = x.Username,
                Email = x.Email.Value,
                Role = x.Role.ToString(),
            });
        }
    }
}

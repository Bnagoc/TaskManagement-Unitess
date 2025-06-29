using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using SharedKernel.Utilities;

namespace Application.Commands.Auth
{
    public class RegisterUserCommand
    {
        private readonly RoleHelper _roleHelper;
        private readonly IUserRepository _userRepository;

        public RegisterUserCommand(IUserRepository userRepository, RoleHelper roleHelper)
        {
            _userRepository = userRepository;
            _roleHelper = roleHelper;
        }

        public async Task<UserDto> ExecuteAsync(RegisterUserInput input)
        {
            if (await _userRepository.ExistsByUsernameAsync(input.Username))
                throw new Exception("Username already exists");

            if (await _userRepository.ExistsByEmailAsync(input.Email))
                throw new Exception("Email already exists");

            var hashedPassword = PasswordHash.Create(input.Password);
            var verifiedEmail = Email.Create(input.Email);

            var user = new User
            {
                Username = input.Username,
                Password = hashedPassword,
                Email = verifiedEmail,
                Role = _roleHelper.GetRoleFromString(input.Role),
                Tasks = []
            };

            await _userRepository.AddAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email.Value,
                Role = user.Role.ToString(),
            };
        }
    }
}

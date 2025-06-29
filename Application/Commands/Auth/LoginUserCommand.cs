using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Commands.Auth
{
    public class LoginUserCommand
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginUserCommand(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<string> ExecuteAsync(LoginUserInput input)
        {
            var user = await _userRepository.GetByEmailAsync(input.Email);

            if (user == null || user.Password.Verify(input.Password))
                throw new Exception("Invalid username or password");

            var token = _jwtTokenService.GenerateToken(user);

            return token;
        }
    }
}

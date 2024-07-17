using LoggingManagerCore.Dtos;
using LoggingManagerCore.Entities;
using LoggingManagerCore.Ports.Primary;
using LoggingManagerCore.Ports.Secundary;

namespace LoggingManagerAdapters.Services
{
    public class AuthService : IAuthService
    {

        IUserRepository userRepository;

        public AuthService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public GenericResponse<LoginDto>? logIn(Credential credential)
        {
            var user = userRepository.login(credential);

            LoginDto loginDto = new LoginDto { User = user.Data, Token = new TokenDto()};

            GenericResponse<LoginDto> response = new GenericResponse<LoginDto> { Data = loginDto, ErrorCode= user.ErrorCode, ErrorMessage=user.ErrorMessage };

            return response;
        }
    }
}

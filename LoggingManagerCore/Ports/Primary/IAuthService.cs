using LoggingManagerCore.Dtos;
using LoggingManagerCore.Entities;

namespace LoggingManagerCore.Ports.Primary
{
    public interface IAuthService
    {
        GenericResponse<LoginDto>? logIn(Credential credential);
    }
}

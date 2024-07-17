using LoggingManagerCore.Dtos;
using LoggingManagerCore.Entities;

namespace LoggingManagerCore.Ports.Secundary
{
    public interface IUserRepository
    {
        GenericResponse<User>? getByUsername(string username);

        GenericResponse<User>? login(Credential credential);

    }
}

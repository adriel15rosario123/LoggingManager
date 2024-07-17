using LoggingManagerCore.Dtos;
using LoggingManagerCore.Entities;

namespace LoggingManagerCore.Ports.Primary
{
    public interface IUserService
    {
        GenericResponse<User>? getByUsername(string username);
    }
}

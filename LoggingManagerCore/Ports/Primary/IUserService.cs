using LoggingManagerCore.Entities;

namespace LoggingManagerCore.Ports.Primary
{
    public interface IUserService
    {
        OracleProcedureResponse<User>? getByUsername(string username);
    }
}

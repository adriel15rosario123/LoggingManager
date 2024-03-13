using LoggingManagerCore.Entities;

namespace LoggingManagerCore.Ports.Secundary
{
    public interface IUserRepository
    {
        OracleProcedureResponse<User>? GetUserByUsername(string username);

        OracleProcedureResponse<User>? Login(Credential credential);
    }
}

using LoggingManagerCore.Entities;

namespace LoggingManagerCore.Ports.Secundary
{
    public interface IUserRepository
    {
        OracleProcedureResponse<User>? getByUsername(string username);

        OracleProcedureResponse<User>? login(Credential credential);

    }
}

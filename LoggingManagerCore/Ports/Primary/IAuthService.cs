using LoggingManagerCore.Entities;

namespace LoggingManagerCore.Ports.Primary
{
    public interface IAuthService
    {
        OracleProcedureResponse<User>? logIn(Credential credential);
    }
}

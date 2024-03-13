using LoggingManagerCore.Entities;
using LoggingManagerCore.Enums;
using LoggingManagerCore.Ports.Secundary;

namespace LoggingManagerAdapters.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IOracleDbContext context;
        public UserRepository(IOracleDbContext context)
        {
            this.context = context;
        }

        public OracleProcedureResponse<User>? GetUserByUsername(string username)
        {
            return context.ExecuteStoreProcedure<string,User>(StoreProcedure.GetUserByUsername, username);
        }

        public OracleProcedureResponse<User>? Login(Credential credential)
        {
            return context.ExecuteStoreProcedure<Credential,User>(StoreProcedure.LoginUser, credential);
        }
    }
}

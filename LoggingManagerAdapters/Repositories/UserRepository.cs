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

        public OracleProcedureResponse<User>? getByUsername(string username)
        {
            return context.ExecuteStoreProcedure<string,User>(StoreProcedure.GetUserByUsername, username);
        }

        public OracleProcedureResponse<User>? login(Credential credential)
        {
            return context.ExecuteStoreProcedure<Credential,User>(StoreProcedure.LoginUser, credential);    
        }

    }
}

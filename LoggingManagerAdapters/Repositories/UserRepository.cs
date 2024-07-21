using LoggingManagerCore.Dtos;
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

        public GenericResponse<User>? getByUsername(string username)
        {
            return context.ExecuteStoreProcedure<string, GenericResponse<User>>(StoreProcedure.GetUserByUsername, username);
        }

        public GenericResponse<User>? login(Credential credential)
        {
            return context.ExecuteStoreProcedure<Credential, GenericResponse<User>>(StoreProcedure.LoginUser, credential);    
        }

    }
}

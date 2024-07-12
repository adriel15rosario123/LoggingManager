using LoggingManagerCore.Entities;
using LoggingManagerCore.Ports.Primary;
using LoggingManagerCore.Ports.Secundary;

namespace LoggingManagerAdapters.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public OracleProcedureResponse<User>? getByUsername(string username)
        {
            return userRepository.getByUsername(username);
        }
    }
}

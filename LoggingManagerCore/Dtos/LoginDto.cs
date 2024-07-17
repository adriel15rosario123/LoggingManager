using LoggingManagerCore.Entities;

namespace LoggingManagerCore.Dtos
{
    public class LoginDto
    {
        public User User { get; set; }
        public TokenDto Token { get; set; }

        public LoginDto(User user,TokenDto token)
        {
            User = user;
            Token = token;
        }

        public LoginDto()
        {
            
        }
    }
}

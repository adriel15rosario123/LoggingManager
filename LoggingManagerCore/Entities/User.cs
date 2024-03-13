namespace LoggingManagerCore.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public UserType UserType { get; set; } = new UserType();
    }
}

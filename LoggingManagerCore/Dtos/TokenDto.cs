namespace LoggingManagerCore.Dtos
{
    public class TokenDto
    {
        public string? Key { get; set; }
        public DateTime? ExpiresAt { get; set; }

        public TokenDto(string key,DateTime expiresAt)
        {
            Key = key;
            ExpiresAt = expiresAt;
        }

        public TokenDto()
        {
            
        }
    }
}

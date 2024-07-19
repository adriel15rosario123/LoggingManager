using System.Text.Json.Serialization;

namespace LoggingManagerCore.Dtos
{
    public class UpdateSystemDto
    {
        [JsonIgnore]
        public int SystemId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? SystemName { get; set; }
    }
}

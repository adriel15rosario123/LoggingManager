namespace LoggingManagerCore.Entities
{
    public class EnrollSystem
    {
        public int SystemId { get; set; }
        public string? SystemUsername { get; set; }
        public string? SystemPassword { get; set; }
        public string? SystemName { get; set; }
        public DateTime? EnrolledDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int ErrorLogs { get; set; }
        public int TrackingLogs { get; set; }
        
    }
}

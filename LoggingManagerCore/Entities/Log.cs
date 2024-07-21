namespace LoggingManagerCore.Entities
{
    public class Log
    {
        public int LogId { get; set; }
        public DateTime LoggingDate { get; set; }
        public string? MethodName { get; set; }
        public string? MethodInput { get; set; }
        public string? MethodOutput { get; set; }
        public string LogType { get; set; }

    }
}

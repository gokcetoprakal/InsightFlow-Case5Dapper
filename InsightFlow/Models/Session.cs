namespace InsightFlow.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AppId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int DurationMinutes { get; set; }
        public string? DayofWeek { get; set; }
    }
}

namespace InsightFlow.Models
{
    public class Productivity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateOnly Date {  get; set; }
        public int ProductivityScore {  get; set; }
        public decimal SleepHours {  get; set; }
    }
}

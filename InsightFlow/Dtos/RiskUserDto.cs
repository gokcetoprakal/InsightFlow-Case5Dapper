namespace InsightFlow.Dtos
{
    public class RiskUserDto
    {
        public string? UserName { get; set; }
        public double ProductivityScore { get; set; }
        public double AvgSleepHours { get; set; }
        public string? MostUsedApp { get; set; }
    }
}

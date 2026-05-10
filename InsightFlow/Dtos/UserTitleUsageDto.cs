namespace InsightFlow.Dtos
{
    public class UserTitleUsageDto
    {
        public string? UserTitle { get; set; }
        public double AvgHours { get; set; } // "7.4h" gibi göstermek için
        public double BarWidthPercentage { get; set; } // En uzun süreye göre genişlik
    }
}

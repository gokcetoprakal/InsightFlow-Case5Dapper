namespace InsightFlow.Dtos
{
    public class AppUsageDto
    {
        public string? AppName { get; set; }
        public int TotalMinutes { get; set; }
        public string? FormattedTime { get; set; } // "3h 20m" formatı için
        public double UsagePercentage { get; set; } // En çok kullanılan uygulamaya göre oran
        public string? CategoryName { get; set; } // Renk belirlemek için gerekebilir
    }
}

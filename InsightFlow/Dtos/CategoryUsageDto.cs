namespace InsightFlow.Dtos
{
    public class CategoryUsageDto
    {
        public string? CategoryName { get; set; }
        public int TotalMinutes { get; set; }
        public double Percentage { get; set; }
        public string? HexColor { get; set; }
    }
}

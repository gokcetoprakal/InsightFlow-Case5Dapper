namespace InsightFlow.Dtos
{
    public class ScreenTimeProductivityDto
    {
        public string? RangeLabel { get; set; } // "0-2h", "4-6h" vb.
        public double AvgProductivity { get; set; } // Bar yüksekliği için 0-100 arası
        public string? BarColorClass { get; set; } // Dinamik renk için
    }
}

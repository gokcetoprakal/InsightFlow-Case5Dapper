using InsightFlow.Dtos;

namespace InsightFlow.Services.ProductivityServices
{
    public interface IProductivityService
    {
        Task<IEnumerable<ProductivityChartDto>> GetWeeklyComparisonAsync();
    }
}

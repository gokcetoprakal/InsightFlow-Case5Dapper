using InsightFlow.Dtos;

namespace InsightFlow.Services.CategoryUsageServices
{
    public interface ICategoryUsageService
    {
        Task<IEnumerable<CategoryUsageDto>> GetCategoryUsageAsync();
    }
}

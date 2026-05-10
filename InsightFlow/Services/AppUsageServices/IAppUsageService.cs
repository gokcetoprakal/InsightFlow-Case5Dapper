using InsightFlow.Dtos;

namespace InsightFlow.Services.AppUsageServices
{
    public interface IAppUsageService
    {
        Task<IEnumerable<AppUsageDto>> GetTopAppDistributionAsync();
    }
}

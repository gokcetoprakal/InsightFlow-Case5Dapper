using InsightFlow.Dtos;

namespace InsightFlow.Services.KeyInsightServices
{
    public interface IKeyInsightService
    {
        Task<IEnumerable<KeyInsightDto>> GetKeyInsightsAsync();
    }
}

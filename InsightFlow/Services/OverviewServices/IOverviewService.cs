using InsightFlow.Dtos;

namespace InsightFlow.Services.OverviewServices
{
    public interface IOverviewService
    {
        Task<DashboardStatsDto> GetDashboardStatsAsync();
    }
}

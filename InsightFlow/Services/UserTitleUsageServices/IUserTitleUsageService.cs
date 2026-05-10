using InsightFlow.Dtos;

namespace InsightFlow.Services.UserTitleUsageServices
{
    public interface IUserTitleUsageService
    {
        Task<IEnumerable<UserTitleUsageDto>> GetAvgScreenTimeByUserTitleAsync();
        Task<IEnumerable<UserTitleSleepDto>> GetAvgSleepByUserTitleAsync();
    }
}

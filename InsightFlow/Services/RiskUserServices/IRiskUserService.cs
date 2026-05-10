using InsightFlow.Dtos;

namespace InsightFlow.Services.RiskUserServices
{
    public interface IRiskUserService
    {
        Task<IEnumerable<RiskUserDto>> GetLowProductivityUsersAsync();
    }
}

using InsightFlow.Dtos;

namespace InsightFlow.Services.ScreenTimeProductivityServices
{
    public interface IScreenTimeProductivityService
    {
        Task<IEnumerable<ScreenTimeProductivityDto>> GetProductivityByScreenTimeAsync();
    }
}

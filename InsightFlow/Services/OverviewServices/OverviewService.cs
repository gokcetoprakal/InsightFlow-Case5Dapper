using Dapper;
using InsightFlow.Context;
using InsightFlow.Dtos;
using System.Data;

namespace InsightFlow.Services.OverviewServices
{
    public class OverviewService : IOverviewService
    {
        public readonly IDbConnection _connection;

        public OverviewService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            string query = @"
        SELECT
            CAST(SUM(DurationMinutes) / 60.0 AS DECIMAL(10,2)) AS TotalScreenTime,

            (
                SELECT COUNT(DISTINCT UserId)
                FROM Sessions
            ) AS ActiveUsers,

            (
                SELECT CAST(AVG(CAST(ProductivityScore AS DECIMAL(10,2))) AS DECIMAL(10,2))
                FROM Productivity
            ) AS AvgProductivityScore
        FROM Sessions";

            return await _connection.QueryFirstOrDefaultAsync<DashboardStatsDto>(query)
       ?? new DashboardStatsDto();
        }
    }
}

using Dapper;
using InsightFlow.Dtos;
using System.Data;

namespace InsightFlow.Services.ScreenTimeProductivityServices
{
    public class ScreenTimeProductivityService : IScreenTimeProductivityService
    {
        public IDbConnection _connection;

        public ScreenTimeProductivityService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<ScreenTimeProductivityDto>> GetProductivityByScreenTimeAsync()
        {
            string sql = @"
        SELECT 
            CASE 
                WHEN s.DurationMinutes <= 120 THEN '0-2h'
                WHEN s.DurationMinutes <= 240 THEN '2-4h'
                WHEN s.DurationMinutes <= 360 THEN '4-6h'
                WHEN s.DurationMinutes <= 480 THEN '6-8h'
                ELSE '8h+' 
            END AS RangeLabel,
            AVG(p.ProductivityScore) AS AvgProductivity
        FROM Sessions s
        INNER JOIN Productivity p ON s.Id = p.Id
        GROUP BY 
            CASE 
                WHEN s.DurationMinutes <= 120 THEN '0-2h'
                WHEN s.DurationMinutes <= 240 THEN '2-4h'
                WHEN s.DurationMinutes <= 360 THEN '4-6h'
                WHEN s.DurationMinutes <= 480 THEN '6-8h'
                ELSE '8h+' 
            END
        ORDER BY MIN(s.DurationMinutes)";

            var result = await _connection.QueryAsync<ScreenTimeProductivityDto>(sql);
            return result;
        }
    }
}

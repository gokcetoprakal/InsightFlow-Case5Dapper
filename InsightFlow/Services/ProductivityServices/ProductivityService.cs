using Dapper;
using InsightFlow.Dtos;
using System.Data;

namespace InsightFlow.Services.ProductivityServices
{
    public class ProductivityService : IProductivityService
    {
        private readonly IDbConnection _connection;

        public ProductivityService(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<ProductivityChartDto>> GetWeeklyComparisonAsync()
        {
            // OVER() fonksiyonu ile haftalık genel ortalamayı her satıra 'WeeklyAvg' olarak ekliyoruz
            string sql = @"
        WITH WeeklyData AS (
            SELECT 
                s.DayofWeek, 
                SUM(s.DurationMinutes) AS ScreenTimeMinutes, 
                AVG(CAST(p.ProductivityScore AS FLOAT)) AS AvgProductivity
            FROM Sessions s
            INNER JOIN Productivity p ON s.UserId = p.UserId 
                AND CAST(s.StartTime AS DATE) = p.Date
            GROUP BY s.DayofWeek
        )
        SELECT *, 
               AVG(AvgProductivity) OVER() as OverallAvg -- Haftalık genel ortalama
        FROM WeeklyData
        ORDER BY 
            CASE DayofWeek 
                WHEN 'Monday' THEN 1 WHEN 'Tuesday' THEN 2 WHEN 'Wednesday' THEN 3 
                WHEN 'Thursday' THEN 4 WHEN 'Friday' THEN 5 WHEN 'Saturday' THEN 6 
                WHEN 'Sunday' THEN 7 
            END";

            return await _connection.QueryAsync<ProductivityChartDto>(sql);
        }
    }
}

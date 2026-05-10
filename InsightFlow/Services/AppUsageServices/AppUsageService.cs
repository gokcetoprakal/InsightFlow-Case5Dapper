using Dapper;
using InsightFlow.Dtos;
using System.Data;

namespace InsightFlow.Services.AppUsageServices
{
    public class AppUsageService : IAppUsageService
    {
        public IDbConnection _connection;

        public AppUsageService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<AppUsageDto>> GetTopAppDistributionAsync()
        {
            string sql = @"
        SELECT TOP 5
            a.AppName,
            SUM(s.DurationMinutes) as TotalMinutes,
            a.Category as CategoryName
        FROM Sessions s
        INNER JOIN Apps a ON s.AppId = a.Id
        GROUP BY a.AppName, a.Category
        ORDER BY TotalMinutes DESC";

            var results = await _connection.QueryAsync<AppUsageDto>(sql);
            var list = results.ToList();

            if (list.Any())
            {
                int maxMinutes = list.Max(x => x.TotalMinutes);
                foreach (var item in list)
                {
                    // Saat ve Dakika formatlama
                    int hours = item.TotalMinutes / 60;
                    int mins = item.TotalMinutes % 60;
                    item.FormattedTime = hours > 0 ? $"{hours}h {mins}m" : $"{mins}m";

                    // En yüksek değere göre bar yüzdesi (Örn: Max 500dk ise, 250dk olan %50 görünür)
                    item.UsagePercentage = (item.TotalMinutes * 100.0) / maxMinutes;
                }
            }
            return list;
        }
    }
}

using Dapper;
using InsightFlow.Dtos;
using System.Data;

namespace InsightFlow.Services.UserTitleUsageServices
{
    public class UserTitleUsageService : IUserTitleUsageService
    {
        public readonly IDbConnection _connection;

        public UserTitleUsageService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<UserTitleUsageDto>> GetAvgScreenTimeByUserTitleAsync()
        {
            string sql = @"
        SELECT 
            u.UserTitle, 
            AVG(s.DurationMinutes / 60.0) AS AvgHours
        FROM Users u
        INNER JOIN Sessions s ON u.Id = s.UserId
        GROUP BY u.UserTitle
        ORDER BY AvgHours DESC";

            var results = await _connection.QueryAsync<UserTitleUsageDto>(sql);
            var list = results.ToList();

            if (list.Any())
            {
                double maxHours = list.Max(x => x.AvgHours);
                foreach (var item in list)
                {
                    // En yüksek değere göre barın genişliğini (oranını) hesaplıyoruz
                    item.BarWidthPercentage = (item.AvgHours * 100.0) / maxHours;
                }
            }

            return list;
        }

        public async Task<IEnumerable<UserTitleSleepDto>> GetAvgSleepByUserTitleAsync()
        {
            string sql = @"
        SELECT 
            u.UserTitle, 
            AVG(p.SleepHours) AS AvgSleepHours
        FROM Users u
        INNER JOIN Productivity p ON u.Id = p.UserId
        GROUP BY u.UserTitle
        ORDER BY AvgSleepHours DESC";

            var results = await _connection.QueryAsync<UserTitleSleepDto>(sql);
            var list = results.ToList();

            if (list.Any())
            {
                // En yüksek uyku saatini %100 kabul edip diğerlerini oranlıyoruz
                double maxSleep = list.Max(x => x.AvgSleepHours);
                foreach (var item in list)
                {
                    item.BarWidthPercentage = (item.AvgSleepHours * 100.0) / maxSleep;
                }
            }

            return list;
        }
    }
}

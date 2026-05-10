using Dapper;
using InsightFlow.Dtos;
using System.Data;

namespace InsightFlow.Services.RiskUserServices
{
    public class RiskUserService : IRiskUserService
    {
        public readonly IDbConnection _connection;

        public RiskUserService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<RiskUserDto>> GetLowProductivityUsersAsync()
        {
            string sql = @"
        SELECT TOP 3 
            (u.Name + ' ' + u.Surname) AS UserName, 
            AVG(p.ProductivityScore) AS ProductivityScore,
            AVG(p.SleepHours) AS AvgSleepHours,
            (SELECT TOP 1 a.AppName 
             FROM Sessions s 
             INNER JOIN Apps a ON s.AppId = a.Id 
             WHERE s.UserId = u.Id 
             GROUP BY a.AppName 
             ORDER BY SUM(s.DurationMinutes) DESC) AS MostUsedApp
        FROM Users u
        INNER JOIN Productivity p ON u.Id = p.UserId
        GROUP BY u.Id, u.Name, u.Surname -- Surname de GROUP BY'a eklenmeli
        ORDER BY AVG(p.ProductivityScore) ASC"; // En düşükten başla

            return await _connection.QueryAsync<RiskUserDto>(sql);
        }
    }
}

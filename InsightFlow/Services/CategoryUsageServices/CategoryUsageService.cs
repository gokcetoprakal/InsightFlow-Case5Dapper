using Dapper;
using InsightFlow.Dtos;
using System.Data;

namespace InsightFlow.Services.CategoryUsageServices
{
    public class CategoryUsageService : ICategoryUsageService
    {
        public readonly IDbConnection _connection;

        public CategoryUsageService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<CategoryUsageDto>> GetCategoryUsageAsync()
        {
            string sql = @"
        WITH TotalTime AS (
            SELECT SUM(DurationMinutes) as GrandTotal FROM Sessions
        )
        SELECT 
            a.Category AS CategoryName, -- SQL'deki 'Category' kolonunu DTO'daki 'CategoryName'e bağladık
            SUM(s.DurationMinutes) as TotalMinutes,
            (SUM(s.DurationMinutes) * 100.0 / (SELECT GrandTotal FROM TotalTime)) as Percentage
        FROM Sessions s
        INNER JOIN Apps a ON s.AppId = a.Id
        GROUP BY a.Category
        ORDER BY TotalMinutes DESC";

            return await _connection.QueryAsync<CategoryUsageDto>(sql);
        }
    }
}

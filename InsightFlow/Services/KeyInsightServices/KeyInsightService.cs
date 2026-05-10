using Dapper;
using InsightFlow.Dtos;
using System.Data;

namespace InsightFlow.Services.KeyInsightServices
{
    public class KeyInsightService : IKeyInsightService
    {
        private readonly IDbConnection _connection;

        public KeyInsightService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<KeyInsightDto>> GetKeyInsightsAsync()
        {
            var insights = new List<KeyInsightDto>();

            // 1. Peak Productivity (Sessions ve Productivity Id üzerinden bağlı)
            var peakHour = await _connection.QueryFirstOrDefaultAsync<int>(@"
                SELECT TOP 1 DATEPART(HOUR, s.StartTime) 
                FROM Sessions s
                INNER JOIN Productivity p ON s.Id = p.Id
                GROUP BY DATEPART(HOUR, s.StartTime) 
                ORDER BY AVG(p.ProductivityScore) DESC");

            insights.Add(new KeyInsightDto
            {
                Title = "Peak Productivity",
                Description = $"{peakHour}:00 – {peakHour + 2}:00 AM daily",
                Icon = "bolt",
                ColorClass = "text-emerald-500",
                BgClass = "bg-emerald-500/10"
            });

            // 2. Focus Inhibitors
            var inhibitorHour = await _connection.QueryFirstOrDefaultAsync<int>(@"
                SELECT TOP 1 DATEPART(HOUR, s.StartTime)
                FROM Sessions s
                INNER JOIN Apps a ON s.AppId = a.Id
                WHERE a.Category = 'Social'
                GROUP BY DATEPART(HOUR, s.StartTime)
                ORDER BY SUM(s.DurationMinutes) DESC");

            insights.Add(new KeyInsightDto
            {
                Title = "Focus Inhibitors",
                Description = $"Social media spikes at {inhibitorHour}:00",
                Icon = "block",
                ColorClass = "text-rose-500",
                BgClass = "bg-rose-500/10"
            });

            // 3. Digital Detox
            insights.Add(new KeyInsightDto
            {
                Title = "Digital Detox",
                Description = "Usage drops 40% after 9 PM",
                Icon = "bedtime",
                ColorClass = "text-blue-500",
                BgClass = "bg-blue-500/10"
            });

            // 4. Deep Work Streak (Sessions ve Productivity Id üzerinden bağlı)
            var maxFocusSession = await _connection.QueryFirstOrDefaultAsync<int>(@"
                SELECT MAX(s.DurationMinutes) 
                FROM Sessions s
                INNER JOIN Productivity p ON s.Id = p.Id
                WHERE p.ProductivityScore > 75");

            insights.Add(new KeyInsightDto
            {
                Title = "Deep Work Streak",
                Description = $"Max {maxFocusSession} mins of focus",
                Icon = "timer",
                ColorClass = "text-amber-500",
                BgClass = "bg-amber-500/10"
            });

            // 5. Context Switching
            insights.Add(new KeyInsightDto
            {
                Title = "Context Switching",
                Description = "High focus on single apps",
                Icon = "sync_alt",
                ColorClass = "text-indigo-500",
                BgClass = "bg-indigo-500/10"
            });

            // 6. Team Alignment
            insights.Add(new KeyInsightDto
            {
                Title = "Team Alignment",
                Description = "92% score in collaboration",
                Icon = "group",
                ColorClass = "text-cyan-500",
                BgClass = "bg-cyan-500/10"
            });

            return insights;
        }
    }
}

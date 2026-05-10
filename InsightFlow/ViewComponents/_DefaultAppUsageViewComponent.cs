using InsightFlow.Services.AppUsageServices;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.ViewComponents
{
    public class _DefaultAppUsageViewComponent : ViewComponent
    {
        private readonly IAppUsageService _appUsageService;

        public _DefaultAppUsageViewComponent(IAppUsageService appUsageService)
        {
            _appUsageService = appUsageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Servisten en çok kullanılan ilk 5 uygulamayı çekiyoruz
            var apps = await _appUsageService.GetTopAppDistributionAsync();
            var appList = apps.ToList();

            if (appList.Any())
            {
                // En yüksek kullanım süresini bul (Bar genişliğini oranlamak için)
                int maxMinutes = appList.Max(x => x.TotalMinutes);

                foreach (var item in appList)
                {
                    // 1. Süre Formatlama: "3h 20m"
                    int hours = item.TotalMinutes / 60;
                    int mins = item.TotalMinutes % 60;
                    item.FormattedTime = hours > 0 ? $"{hours}h {mins}m" : $"{mins}m";

                    // 2. Bar Genişliği: En çok kullanılana göre % kaç?
                    // (Örn: En çok kullanılan 100dk ise, 50dk olan %50 genişlikte görünür)
                    item.UsagePercentage = (item.TotalMinutes * 100.0) / maxMinutes;
                }
            }

            return View(appList);
        }
    }
}

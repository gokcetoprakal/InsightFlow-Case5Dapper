using InsightFlow.Services.UserTitleUsageServices;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.ViewComponents
{
    public class _DefaultAvgSleepHourViewComponent : ViewComponent
    {
        public readonly IUserTitleUsageService _services;

        public _DefaultAvgSleepHourViewComponent(IUserTitleUsageService services)
        {
            _services = services;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _services.GetAvgSleepByUserTitleAsync();
            return View(data.ToList());
        }
    }
}

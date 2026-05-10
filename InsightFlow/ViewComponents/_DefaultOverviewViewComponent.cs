using InsightFlow.Services.OverviewServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InsightFlow.ViewComponents
{
    public class _DefaultOverviewViewComponent : ViewComponent
    {
        private readonly IOverviewService _service;

        public _DefaultOverviewViewComponent(IOverviewService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _service.GetDashboardStatsAsync();
            ViewBag.TotalScreenTime = values.TotalScreenTime;
            ViewBag.ActiveUsers = values.ActiveUsers;
            ViewBag.AvgProductivityScore = values.AvgProductivityScore;
            return View(values);
        }
    }
}

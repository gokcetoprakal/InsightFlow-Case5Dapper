using InsightFlow.Services.KeyInsightServices;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.ViewComponents
{
    public class _DefaultKeyInsightsViewComponent : ViewComponent
    {
        private readonly IKeyInsightService _insightService;

        public _DefaultKeyInsightsViewComponent(IKeyInsightService insightService)
        {
            _insightService = insightService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _insightService.GetKeyInsightsAsync();
            return View(data.ToList());
        }
    }
}

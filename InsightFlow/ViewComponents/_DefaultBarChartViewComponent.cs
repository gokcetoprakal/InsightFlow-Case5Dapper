using InsightFlow.Dtos;
using InsightFlow.Services.ProductivityServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InsightFlow.ViewComponents
{
    public class _DefaultBarChartViewComponent : ViewComponent
    {
        private readonly IProductivityService _productivityService;

        public _DefaultBarChartViewComponent(IProductivityService productivityService)
        {
            _productivityService = productivityService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _productivityService.GetWeeklyComparisonAsync();

            var dataList = result.ToList();

            if (dataList.Any())
            {
                ViewBag.WeeklyAverage = dataList.Average(x => x.AvgProductivity);
            }
            else
            {
                ViewBag.WeeklyAverage = 0;
            }
            return View(dataList);
        }
    }
}

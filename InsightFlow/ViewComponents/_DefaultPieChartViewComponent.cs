using InsightFlow.Services.CategoryUsageServices;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.ViewComponents
{
    public class _DefaultPieChartViewComponent : ViewComponent
    {
        public readonly ICategoryUsageService _categoryService;

        public _DefaultPieChartViewComponent(ICategoryUsageService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _categoryService.GetCategoryUsageAsync();
            return View(data.ToList());
        }
    }
}

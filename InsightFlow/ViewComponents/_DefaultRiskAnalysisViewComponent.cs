using InsightFlow.Services.RiskUserServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InsightFlow.ViewComponents
{
    public class _DefaultRiskAnalysisViewComponent : ViewComponent
    {
        public readonly IRiskUserService _userService;

        public _DefaultRiskAnalysisViewComponent(IRiskUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _userService.GetLowProductivityUsersAsync();
            return View(data.ToList());
        }
    }
}

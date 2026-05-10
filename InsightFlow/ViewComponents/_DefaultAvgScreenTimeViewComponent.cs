using InsightFlow.Services.UserTitleUsageServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InsightFlow.ViewComponents
{
    public class _DefaultAvgScreenTimeViewComponent : ViewComponent
    {
        public readonly IUserTitleUsageService _services;

        public _DefaultAvgScreenTimeViewComponent(IUserTitleUsageService services)
        {
            _services = services;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _services.GetAvgScreenTimeByUserTitleAsync();
            return View(data.ToList());
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.ViewComponents
{
    public class _DefaultFooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

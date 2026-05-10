using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.ViewComponents
{
    public class _DefaultNavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

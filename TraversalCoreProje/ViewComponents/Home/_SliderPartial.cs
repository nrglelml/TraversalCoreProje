


using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.ViewComponents.Home
{
    public class _SliderPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

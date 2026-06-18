using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.ViewComponents.Home
{
    public class _SubAbout:ViewComponent
    {
        SubAboutManager sm = new SubAboutManager(new EfSubAboutDal());
        public IViewComponentResult Invoke()
        {
            var values = sm.TGetList().FirstOrDefault();
            return View(values);
        }
    }
}

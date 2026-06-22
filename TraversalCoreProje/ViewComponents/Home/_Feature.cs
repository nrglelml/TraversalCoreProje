using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.ViewComponents.Home
{
    public class _Feature:ViewComponent
    {
        FeatureManager fm=new FeatureManager(new EfFeatureDal());
        public IViewComponentResult Invoke()
        {
            var values=fm.TGetListByStatus(true);
            return View(values);
        }
    }
}

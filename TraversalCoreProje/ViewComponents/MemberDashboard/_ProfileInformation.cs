using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.ViewComponents.MemberDashboard
{
    public class _ProfileInformation:ViewComponent
    {
     
        public  IViewComponentResult Invoke()
        {
                   
            return View();
        }
    }
}

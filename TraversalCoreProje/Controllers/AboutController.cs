using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Controllers
{
    [AllowAnonymous]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private readonly IAbout2Service _about2Service;

        public AboutController(IAboutService aboutService, IAbout2Service about2Service)
        {
            _aboutService = aboutService;
            _about2Service = about2Service;
        }

        public IActionResult Index()
        {
            var about = _aboutService.TGetListByStatus(true).FirstOrDefault(x => x.AboutStatus == true);
            var about2List = _about2Service.TGetListByStatus(true).Where(x => x.About2Status == true).ToList();

            ViewBag.About2List = about2List;

            return View(about);
        }
    }
}

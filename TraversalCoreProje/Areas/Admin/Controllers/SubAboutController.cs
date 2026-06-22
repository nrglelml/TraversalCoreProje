using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    public class SubAboutController : Controller
    {
        private readonly ISubAboutService _subAboutService;
        public SubAboutController (ISubAboutService subAboutService)
        {
            _subAboutService = subAboutService;
        }
        public IActionResult Index()
        {
            var values = _subAboutService.TGetListByStatus(true).FirstOrDefault();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddSubAbout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddSubAbout(SubAbout p)
        {
            p.Status = true;
            _subAboutService.TAdd(p);
            return RedirectToAction("Index", "SubAbout", new { area = "Admin" });
        }
        [HttpGet]
        public IActionResult UpdateSubAbout(int id)
        {
            var value=_subAboutService.TGetByID(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateSubAbout(SubAbout p)
        {
            p.Status = true;
            _subAboutService.TUpdate(p);
            return RedirectToAction("Index", "SubAbout", new { area = "Admin" });
        }
        public IActionResult DeleteSubAbout(int id)
        {
            var value = _subAboutService.TGetByID(id);
            value.Status = false;
            _subAboutService.TUpdate(value);
            return RedirectToAction("Index", "SubAbout", new { area = "Admin" });
        }

    }
    
}

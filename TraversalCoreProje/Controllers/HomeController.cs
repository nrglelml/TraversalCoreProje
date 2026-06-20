using BusinessLayer.Abstract;
using DocumentFormat.OpenXml.Spreadsheet;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace TraversalCoreProje.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly INewsletterService _newsletterService;
        private readonly IGuideService _guideService;
        public HomeController(INewsletterService newsletterService, IGuideService guideService)
        {
            _newsletterService = newsletterService;
            _guideService = guideService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNewsLetter(string Mail)
        {
            if (string.IsNullOrEmpty(Mail))
            {
                TempData["Error"] = "Lütfen geçerli bir e-posta giriniz.";
                return Redirect(Request.Headers["Referer"].ToString()); // geldiği sayfaya geri dön
            }
            _newsletterService.TAdd(new Newsletter() { Mail = Mail,Status=true });
            TempData["Success"] = "Abone oldunuz!";
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Guides()
        {
            var values = _guideService.TGetListByStatus(true);
            return View(values);
        }
     
    }
}

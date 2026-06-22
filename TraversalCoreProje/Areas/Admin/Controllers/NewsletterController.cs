using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class NewsletterController : BaseAdminController
    {
        private readonly INewsletterService _newsletterService;
        public NewsletterController(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }
        public IActionResult Index()
        {
            var values = _newsletterService.TGetList();
            return View(values);
        }
    }
}

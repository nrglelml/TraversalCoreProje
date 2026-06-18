using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class ContactUsController : Controller
    {
        private readonly IContactUsService _contactUsService;

        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        public IActionResult Index()
        {
            var values = _contactUsService.TGetListContactUsByStatus(true);
            return View(values);
        }
        public IActionResult DeletedMessages()
        {
            var values = _contactUsService.TGetListContactUsByStatus(false);
            return View(values);
        }
        public IActionResult MakePassive(int id)
        {
            //statusu false yaparak siliyoruz
            _contactUsService.ChangeStatus(id, false);
            return RedirectToAction("Index", "ContactUs", new { area = "Admin" });
        }
        public IActionResult Delete(int id)
        {
            var value=_contactUsService.TGetByID(id);
            _contactUsService.TDelete(value);
            return RedirectToAction("DeletedMessages", "ContactUs", new { area = "Admin" });
        }
        [HttpGet]
        public IActionResult MessageDetails(int id)
        {
            var value = _contactUsService.TGetByID(id);
            return View(value);
        }
    }
}

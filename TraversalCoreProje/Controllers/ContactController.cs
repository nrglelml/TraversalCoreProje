using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IContactUsService _contactUsService;
        public ContactController (IContactService contactService, IContactUsService contactUsService)
        {
            _contactService = contactService;
            _contactUsService = contactUsService;
        }
        public IActionResult Index()
        {
            var values=_contactService.TGetListByStatus(true).FirstOrDefault();
            return View(values);
        }
        [HttpPost]
        public IActionResult ContactForm(ContactUs p)
        {
            p.MessageStatus = true;
            p.MessageDate = DateTime.Now;
            _contactUsService.TAdd(p);
            return RedirectToAction("Index");
        }
    }
}

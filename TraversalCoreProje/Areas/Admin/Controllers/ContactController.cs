using BusinessLayer.Abstract;
using DocumentFormat.OpenXml.Office2010.Excel;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public class ContactController : BaseAdminController
    {
        private readonly IContactService _contactService;
        public ContactController (IContactService contactService)
        {
            _contactService = contactService;
        }
        public IActionResult Index()
        {
            var values=_contactService.TGetListByStatus(true).FirstOrDefault();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddContact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddContact(Contact p)
        {
            p.ContactStatus = true;
            _contactService.TAdd(p);
            return RedirectToAction("Index", "Contact", new { area="Admin" });
        }
        [HttpGet]
        public IActionResult UpdateContact(int id)
        {
            var contact=_contactService.TGetByID(id);
            return View(contact);
        }
        [HttpPost]
        public IActionResult UpdateContact(Contact p)
        {
            p.ContactStatus = true;
            _contactService.TUpdate(p);
            return RedirectToAction("Index", "Contact", new { area = "Admin" });
        }
        public IActionResult DeleteContact(int id)
        {
            var contact = _contactService.TGetByID(id);
            contact.ContactStatus = false;
            _contactService.TUpdate(contact);
            return RedirectToAction("Index", "Contact", new { area = "Admin" });
        }
    }
}

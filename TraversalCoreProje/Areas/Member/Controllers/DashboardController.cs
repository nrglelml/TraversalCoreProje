using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TraversalCoreProje.Areas.Member.Models;

namespace TraversalCoreProje.Areas.Member.Controllers
{
    [Area("Member")]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _usermanager;
        public DashboardController(UserManager<AppUser> usermanager)
        {
            _usermanager = usermanager;
        }
        public async Task<IActionResult> Index()
        {
            var values = await _usermanager.FindByNameAsync(User.Identity.Name);

            ViewBag.username = values.Name + " " + values.Surname;
            ViewBag.phone = values.PhoneNumber;
            ViewBag.mail = values.Email;
            ViewBag.image = values.ImageURL;

            return View();
        }
    }
}

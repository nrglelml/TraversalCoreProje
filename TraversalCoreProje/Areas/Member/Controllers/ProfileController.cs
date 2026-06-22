using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TraversalCoreProje.Areas.Member.Models;

namespace TraversalCoreProje.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _usermanager;
        public ProfileController (UserManager<AppUser> usermanager)
        {
            _usermanager = usermanager;
        }
        [HttpGet]
        public async Task< IActionResult> Index()
        {
            var values =await _usermanager.FindByNameAsync(User.Identity.Name);
            UserEditViewModel userEditViewModel = new UserEditViewModel();
            userEditViewModel.name = values.Name;
            userEditViewModel.surname = values.Surname;
            userEditViewModel.phonenumber = values.PhoneNumber;
            userEditViewModel.mail= values.Email;
            userEditViewModel.imageurl= values.ImageURL;

            return View(userEditViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserEditViewModel p)
        {
            var user= await _usermanager.FindByNameAsync(User.Identity.Name);
            if (p.Image != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(p.Image.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = Path.Combine(resource, "wwwroot", "userImages", imagename);

                using (var stream = new FileStream(savelocation, FileMode.Create))
                {
                    await p.Image.CopyToAsync(stream);
                }
                user.ImageURL = imagename;
            }
            user.Name = p.name;
            user.Surname = p.surname;
            if (!string.IsNullOrEmpty(p.password))
            {
                user.PasswordHash = _usermanager.PasswordHasher.HashPassword(user, p.password);
            }
            user.PhoneNumber = p.phonenumber;
            var result =await _usermanager.UpdateAsync(user);
            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}

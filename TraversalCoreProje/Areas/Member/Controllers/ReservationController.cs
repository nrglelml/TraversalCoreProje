using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TraversalCoreProje.Areas.Member.Controllers
{
    [Area("Member")]
    public class ReservationController : Controller
    {
        DestinationManager dm = new DestinationManager(new EfDestinationDal());
        private readonly IReservationService _reservationService;
        private readonly UserManager<AppUser> _usermanager;

        public ReservationController(UserManager<AppUser> usermanager, IReservationService reservationService)
        {
            _usermanager = usermanager;
            _reservationService = reservationService;
        }
        public async Task<IActionResult> MyCurrentReservation()
        {
            var user = await _usermanager.FindByNameAsync(User.Identity.Name);
            var values = _reservationService.GetListWithReservationByStatus(user.Id, "onaylandı");
            return View(values);
        }
        public async Task<IActionResult> MyOldReservation()
        {
            var user = await _usermanager.FindByNameAsync(User.Identity.Name);
            var values = _reservationService.GetListWithReservationByStatus(user.Id, "geçmiş rezervasyon");
            return View(values);
        }
        public async Task<IActionResult> MyApprovalReservation()
        {
            var user = await _usermanager.FindByNameAsync(User.Identity.Name);
            var values = _reservationService.GetListWithReservationByStatus(user.Id, "onay bekliyor");
            return View(values);
        }
        public async Task<IActionResult> MyRejectedReservation()
        {
            var user = await _usermanager.FindByNameAsync(User.Identity.Name);
            var values = _reservationService.GetListWithReservationByStatus(user.Id, "reddedildi");
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> NewReservation()
        {
            var user = await _usermanager.FindByNameAsync(User.Identity.Name);
            List<SelectListItem> values = (from x in dm.TGetList()
                                           select new SelectListItem
                                           {
                                               Text = x.DestinationCity,
                                               Value = x.DestinationID.ToString(),

                                           }).ToList();
            ViewBag.v = values;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewReservation(Reservation p)
        {
            var user = await _usermanager.FindByNameAsync(User.Identity.Name);
            p.AppUserID = user.Id;
            p.Status = "onay bekliyor";
           _reservationService.TAdd(p);
            return RedirectToAction("MyApprovalReservation");
        }
    }
}

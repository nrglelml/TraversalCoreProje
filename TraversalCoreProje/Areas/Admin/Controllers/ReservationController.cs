using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public IActionResult Index()
        {
            var values = _reservationService.TGetListWithDestination();
            return View(values);
        }

        public IActionResult Approve(int id)
        {
            var reservation = _reservationService.TGetByID(id);
            reservation.Status = "onaylandı";
            _reservationService.TUpdate(reservation);
            return RedirectToAction("Index");
        }

        public IActionResult Reject(int id)
        {
            var reservation = _reservationService.TGetByID(id);
            reservation.Status = "reddedildi";
            _reservationService.TUpdate(reservation);
            return RedirectToAction("Index");
        }

        public IActionResult SetPast(int id)
        {
            var reservation = _reservationService.TGetByID(id);
            reservation.Status = "geçmiş rezervasyon";
            _reservationService.TUpdate(reservation);
            return RedirectToAction("Index");
        }
    }
}
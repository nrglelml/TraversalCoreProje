using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : BaseAdminController
    {
        private readonly IAppUserService _appUserService;
        private readonly IReservationService _reservationService;
        private readonly UserManager<AppUser> _usermanager;
        private readonly ICommentService _commentService;

        public UserController(IAppUserService appUserService, IReservationService reservationService, UserManager<AppUser> usermanager, ICommentService commentService )
        {
            _appUserService = appUserService;
            _reservationService = reservationService;
            _usermanager = usermanager;
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            var values = _appUserService.TGetList();
            return View(values);
        }
        public IActionResult DeleteUser(int id)
        {
            var values = _appUserService.TGetByID(id);
            _appUserService.TDelete(values);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var values = _appUserService.TGetByID(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult EditUser(AppUser appUser)
        {
            _appUserService.TUpdate(appUser);
            return RedirectToAction("Index");
        }

        public IActionResult CommentUser(int id)
        {
            var values = _commentService.TGetUserCommentsList(id);
            return View(values);
        }

        public IActionResult ReservationUser(int id)
        {
            var values = _reservationService.GetListWithReservationByStatus(id, "geçmiş rezervasyon");
            
            return View(values);
        }

    }
}

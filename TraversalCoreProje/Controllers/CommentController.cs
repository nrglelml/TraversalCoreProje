using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Controllers
{
    [AllowAnonymous]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;
        public CommentController(ICommentService commentService, UserManager<AppUser> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }
    
        [HttpPost]
        public async Task<IActionResult> AddComment(Comment p)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("SignIn", "Login");
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            p.AppUserID = user.Id;
            p.Date = DateTime.Now;
            p.State = true;

            _commentService.TAdd(p);
            return RedirectToAction("DestinationDetails", "Destination", new { id = p.DestinationID });

        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize]
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

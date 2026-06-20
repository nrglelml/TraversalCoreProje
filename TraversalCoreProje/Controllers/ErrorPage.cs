using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Controllers
{
    [AllowAnonymous]
    public class ErrorPage : Controller
    {

        public IActionResult Error404(int code)
        {
            return View();
        }
    }
}

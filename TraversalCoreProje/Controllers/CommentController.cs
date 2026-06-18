using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Controllers
{
    [AllowAnonymous]
    public class CommentController : Controller
    {
        CommentManager cm = new CommentManager(new EfCommentDal());
        [HttpGet]
        public PartialViewResult AddComment(int id)
        {
            ViewBag.i= id;  
            var values = cm.TGetByID(id);
            return PartialView(values);
        }
        [HttpPost]
        public IActionResult AddComment(Comment p)
        {
            p.Date = DateTime.Now;
            p.State = true;
            cm.TAdd(p);
            return RedirectToAction("Index","Destination");
        }
    }
}

using BusinessLayer.Abstract;
using DocumentFormat.OpenXml.Spreadsheet;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Comment = EntityLayer.Concrete.Comment;

public class _AddCommentViewComponent : ViewComponent
{
    private readonly UserManager<AppUser> _userManager;

    public _AddCommentViewComponent(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync(int id)
    {
        ViewBag.i = id;

        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.username = user.Name + " " + user.Surname;
        }

        return View(new Comment());
    }
}


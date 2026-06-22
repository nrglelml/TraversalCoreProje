using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize]
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;

        public DestinationController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }
        public IActionResult Index()
        {
            var values = _destinationService.TGetListWithGuide();
            return View(values);
        }
        public IActionResult Details(int id)
        {
            var destination = _destinationService.TGetListWithGuide().FirstOrDefault(x => x.DestinationID == id);
            if (destination == null)
            {
                return RedirectToAction("Index");
            }
            return View(destination);
        }
    }
}

using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DTOLayer.DTOs.DestinationDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class DestinationController : Controller
    {
        DestinationManager dm = new DestinationManager(new EfDestinationDal());
        public IActionResult Index()
        {

            var values = dm.TGetList();
            return View(values);
        }
        public IActionResult Details(int id)
        {
            var values = dm.TGetByID(id);
            var model = new DestinationUpdateDTO
            {
                DestinationID = values.DestinationID,
                DestinationCity = values.DestinationCity,
                DestinationDayNight = values.DestinationDayNight,
                DestinationDescription = values.DestinationDescription,
                Details1 = values.Details1,
                Details2 = values.Details2,
                DestinationPrice = values.DestinationPrice,
                DestinationCapacity = values.DestinationCapacity,
                DestinationImage = values.DestinationImage,
                CoverImage = values.CoverImage,
                Image2 = values.Image2
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateDestination(DestinationUpdateDTO p)
        {
            var destination = dm.TGetByID(p.DestinationID);

            destination.DestinationCity = p.DestinationCity;
            destination.DestinationDayNight = p.DestinationDayNight;
            destination.DestinationDescription = p.DestinationDescription;
            destination.Details1 = p.Details1;
            destination.Details2 = p.Details2;
            destination.DestinationPrice = p.DestinationPrice;
            destination.DestinationCapacity = p.DestinationCapacity;

            var resource = Directory.GetCurrentDirectory();

            if (p.DestinationImageFile != null)
            {
                var extension = Path.GetExtension(p.DestinationImageFile.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = Path.Combine(resource, "wwwroot", "destinationImages", imagename);
                using (var stream = new FileStream(savelocation, FileMode.Create))
                {
                    p.DestinationImageFile.CopyTo(stream);
                }
                destination.DestinationImage = imagename;
            }

            if (p.CoverImageFile != null)
            {
                var extension = Path.GetExtension(p.CoverImageFile.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = Path.Combine(resource, "wwwroot", "destinationImages", imagename);
                using (var stream = new FileStream(savelocation, FileMode.Create))
                {
                    p.CoverImageFile.CopyTo(stream);
                }
                destination.CoverImage = imagename;
            }

            if (p.Image2File != null)
            {
                var extension = Path.GetExtension(p.Image2File.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = Path.Combine(resource, "wwwroot", "destinationImages", imagename);
                using (var stream = new FileStream(savelocation, FileMode.Create))
                {
                    p.Image2File.CopyTo(stream);
                }
                destination.Image2 = imagename;
            }
            destination.DestinationStatus = true;

            dm.TUpdate(destination);
            return RedirectToAction("Details", new { id = destination.DestinationID });
        }
        [HttpPost]
        public IActionResult DeleteDestination(int id)
        {
            var destination = dm.TGetByID(id);
            destination.DestinationStatus = false;
            dm.TUpdate(destination);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddDestination()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDestination(DestinationAddDTO p)
        {
            Destination destination = new Destination()
            {
                DestinationCity = p.DestinationCity,
                DestinationDayNight = p.DestinationDayNight,
                DestinationDescription = p.DestinationDescription,
                Details1 = p.Details1,
                Details2 = p.Details2,
                DestinationPrice = p.DestinationPrice,
                DestinationCapacity = p.DestinationCapacity,
                DestinationStatus = true
            };

            var resource = Directory.GetCurrentDirectory();

            if (p.DestinationImageFile != null)
            {
                var extension = Path.GetExtension(p.DestinationImageFile.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = Path.Combine(resource, "wwwroot", "destinationImages", imagename);
                using (var stream = new FileStream(savelocation, FileMode.Create))
                {
                    p.DestinationImageFile.CopyTo(stream);
                }
                destination.DestinationImage = imagename;
            }

            if (p.CoverImageFile != null)
            {
                var extension = Path.GetExtension(p.CoverImageFile.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = Path.Combine(resource, "wwwroot", "destinationImages", imagename);
                using (var stream = new FileStream(savelocation, FileMode.Create))
                {
                    p.CoverImageFile.CopyTo(stream);
                }
                destination.CoverImage = imagename;
            }

            if (p.Image2File != null)
            {
                var extension = Path.GetExtension(p.Image2File.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = Path.Combine(resource, "wwwroot", "destinationImages", imagename);
                using (var stream = new FileStream(savelocation, FileMode.Create))
                {
                    p.Image2File.CopyTo(stream);
                }
                destination.Image2 = imagename;
            }

            dm.TAdd(destination);
            return RedirectToAction("Index");
        }
    }
}

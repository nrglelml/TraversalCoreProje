using AutoMapper;
using BusinessLayer.Abstract;
using DTOLayer.DTOs.About2DTOs;
using DTOLayer.DTOs.AboutDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    public class About2Controller : Controller
    {
        private readonly IAbout2Service _about2Service;
        private readonly IMapper _mapper;
        public About2Controller(IAbout2Service aboutService, IMapper mapper)
        {
            _about2Service = aboutService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var values = _about2Service.TGetListByStatus(true);
            return View(values);
        }
        [HttpGet]
        public IActionResult AddAbout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddAbout(About2AddEditDTO p)
        {
            About2 about = _mapper.Map<About2>(p);
            about.About2Status = true;

            if (p.ImageFile != null)
            {
                about.Image = SaveImage(p.ImageFile);
            }
            _about2Service.TAdd(about);
            return RedirectToAction("Index", "About2", new { area = "Admin" });


        }
        [HttpGet]
        public IActionResult UpdateAbout(int id)
        {
            var about = _about2Service.TGetByID(id); ;
            About2AddEditDTO model = _mapper.Map<About2AddEditDTO>(about);
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateAbout(About2AddEditDTO p)
        {
            var about = _about2Service.TGetByID(p.About2ID);

            about.Title = p.Title;
            about.Description = p.Description;
            about.About2Status = true;

            if (p.ImageFile != null)
            {
                about.Image = SaveImage(p.ImageFile);
            }
            _about2Service.TUpdate(about);

            return RedirectToAction("Index", "About2", new { area = "Admin" });
        }
        public IActionResult DeleteAbout(int id)
        {
            var about = _about2Service.TGetByID(id);
            about.About2Status = false;
            _about2Service.TUpdate(about);
            return RedirectToAction("Index", "About2", new { area = "Admin" });
        }


        private string SaveImage(IFormFile imageFile)
        {
            var resource = Directory.GetCurrentDirectory();
            var extension = Path.GetExtension(imageFile.FileName);
            var imagename = Guid.NewGuid() + extension;
            var savelocation = Path.Combine(resource, "wwwroot", "aboutImages", imagename);

            using (var stream = new FileStream(savelocation, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return imagename;
        }
    }
}


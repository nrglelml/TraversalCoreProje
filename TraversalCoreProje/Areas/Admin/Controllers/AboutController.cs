using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DTOLayer.DTOs.AboutDTOs;
using DTOLayer.DTOs.GuideDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class AboutController : BaseAdminController
    {
        private readonly IAboutService _aboutService;
        private readonly IMapper _mapper;
        public AboutController(IAboutService aboutService, IMapper mapper)
        {
            _aboutService = aboutService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var values = _aboutService.TGetListByStatus(true).FirstOrDefault();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddAbout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddAbout(AboutAddEditDTO p)
        {
            About about = _mapper.Map<About>(p);
            about.AboutStatus = true;

            if (p.ImageFile != null)
            {
                about.Image = SaveImage(p.ImageFile);
            }
            _aboutService.TAdd(about);
            return RedirectToAction("Index", "About", new { area = "Admin" });


        }
        [HttpGet]
        public IActionResult UpdateAbout(int id)
        {
            var about = _aboutService.TGetByID(id);;
            AboutAddEditDTO model = _mapper.Map<AboutAddEditDTO>(about);
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateAbout(AboutAddEditDTO p)
        {
            var about = _aboutService.TGetByID(p.AboutID);

            about.Title = p.Title;
            about.Description = p.Description;
            about.Title2 = p.Title2;
            about.Description2 = p.Description2;
            about.AboutStatus = true;

            if (p.ImageFile != null)
            {
                about.Image = SaveImage(p.ImageFile);
            }
            _aboutService.TUpdate(about);

            return RedirectToAction("Index", "About", new { area = "Admin" });
        }
        public IActionResult DeleteAbout(int id)
        {
            var about= _aboutService.TGetByID(id);
            about.AboutStatus=false;
            _aboutService.TUpdate(about);
            return RedirectToAction("Index", "About", new { area = "Admin" });
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

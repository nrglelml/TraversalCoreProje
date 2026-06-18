using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DocumentFormat.OpenXml.Wordprocessing;
using DTOLayer.DTOs.GuideDTOs;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraversalCoreProje.Areas.Admin.Models;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]

    [AllowAnonymous]
    public class GuideController : Controller
    {
        private readonly IGuideService _guideService;
        private readonly IMapper _mapper;

        public GuideController(IGuideService guideService, IMapper mapper)
        {
            _guideService = guideService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var values = _guideService.TGetList();
            return View(values);
        }
      
        [HttpGet]
        public IActionResult AddGuide()
        {
            return View();
        }



        [HttpPost]
        public IActionResult AddGuide(GuideAddEditDTO p)
        {
            //{
            //    Name = p.Name,
            //    Description = p.Description,
            //    TwitterUrl = p.TwitterUrl,
            //    InstagramUrl = p.InstagramUrl,
            //    GuideStatus = true
            //}
            Guide guide = _mapper.Map<Guide>(p);
            guide.GuideStatus = true;

            if (p.ImageFile != null)
            {
                guide.Image = SaveImage(p.ImageFile);
            }

            GuideValidator validationRules = new GuideValidator();
            ValidationResult result = validationRules.Validate(guide);

            if (result.IsValid)
            {
                _guideService.TAdd(guide);
                return RedirectToAction("Index", "Guide", new { area = "Admin" });
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(p);
            }
        }
        [HttpGet]
        public IActionResult EditGuide(int id)
        {
            var guide = _guideService.TGetByID(id);
            GuideAddEditDTO model = _mapper.Map<GuideAddEditDTO>(guide);
            //GuideAddEditDTO model = new GuideAddEditDTO();

            //{
            //    GuideID = guide.GuideID,
            //    Name = guide.Name,
            //    Description = guide.Description,
            //    TwitterUrl = guide.TwitterUrl,
            //    InstagramUrl = guide.InstagramUrl,
            //    GuideStatus = guide.GuideStatus,
            //    Image = guide.Image
            //}
            
            return View(model);
        }

        [HttpPost]
        public IActionResult EditGuide(GuideAddEditDTO p)
        {
            var guide = _guideService.TGetByID(p.GuideID);

            guide.Name = p.Name;
            guide.Description = p.Description;
            guide.TwitterUrl = p.TwitterUrl;
            guide.InstagramUrl = p.InstagramUrl;

            if (p.ImageFile != null)
            {
                guide.Image = SaveImage(p.ImageFile);
            }

            _guideService.TUpdate(guide);
            return RedirectToAction("Index", "Guide", new { area = "Admin" });
        }



        public IActionResult MakeActive(int id)
        {
            _guideService.ChangeStatus(id, true);
            return RedirectToAction("Index", "Guide", new { area = "Admin" });
        }
      

        public IActionResult MakePassive(int id)
        {
            _guideService.ChangeStatus(id, false);
            return RedirectToAction("Index", "Guide", new { area = "Admin" });
        }
        // ---------------- HELPER ----------------
        private string SaveImage(IFormFile imageFile)
        {
            var resource = Directory.GetCurrentDirectory();
            var extension = Path.GetExtension(imageFile.FileName);
            var imagename = Guid.NewGuid() + extension;
            var savelocation = Path.Combine(resource, "wwwroot", "guideImages", imagename);

            using (var stream = new FileStream(savelocation, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return imagename;
        }

    }
}

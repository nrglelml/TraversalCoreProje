using AutoMapper;
using BusinessLayer.Abstract;
using DTOLayer.DTOs.AboutDTOs;
using DTOLayer.DTOs.FeatureDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController : BaseAdminController
    {
        private readonly IFeatureService _featureService;
        private readonly IMapper _mapper;

        public FeatureController(IFeatureService featureService, IMapper mapper)
        {
            _featureService = featureService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var values = _featureService.TGetListByStatus(true);
            return View(values);
        }
        [HttpGet]
        public IActionResult AddFeature()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddFeature(FeatureAddEditDTO p)
        {
            if (p.IsBigImage) { ResetOtherBigImages(); }
            Feature feature = _mapper.Map<Feature>(p);
            feature.FeatureStatus = true;

            if (p.ImageFile != null)
            {
                feature.Image = SaveImage(p.ImageFile);
            }
            _featureService.TAdd(feature);
            return RedirectToAction("Index", "Feature", new { area = "Admin" });


        }
        [HttpGet]
        public IActionResult UpdateFeature(int id)
        {
            var feature = _featureService.TGetByID(id); ;
            FeatureAddEditDTO model = _mapper.Map<FeatureAddEditDTO>(feature);
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateFeature(FeatureAddEditDTO p)
        {
            if (p.IsBigImage) { ResetOtherBigImages(); }
            var feature = _featureService.TGetByID(p.FeatureID);

            feature.Title = p.Title;
            feature.Description = p.Description;
            feature.IsBigImage= p.IsBigImage;
            feature.FeatureStatus = true;

            if (p.ImageFile != null)
            {
                feature.Image = SaveImage(p.ImageFile);
            }
            _featureService.TUpdate(feature);

            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }
        public IActionResult DeleteFeature(int id)
        {
            var feature = _featureService.TGetByID(id);
            feature.FeatureStatus = false;
            _featureService.TUpdate(feature);
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }


        private string SaveImage(IFormFile imageFile)
        {
            var resource = Directory.GetCurrentDirectory();
            var extension = Path.GetExtension(imageFile.FileName);
            var imagename = Guid.NewGuid() + extension;
            var savelocation = Path.Combine(resource, "wwwroot", "featureImages", imagename);

            using (var stream = new FileStream(savelocation, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return imagename;
        }
        private void ResetOtherBigImages()
        {
      
            var allFeatures = _featureService.TGetList();
            foreach (var item in allFeatures)
            {
                if (item.IsBigImage)
                {
                    item.IsBigImage = false;
                    _featureService.TUpdate(item);
                }
            }
        }
    }
}

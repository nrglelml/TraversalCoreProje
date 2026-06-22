using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.FeatureDTOs
{
    public class FeatureAddEditDTO
    {
        public int FeatureID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsBigImage { get; set; }

        public bool FeatureStatus { get; set; }

        public string? Image { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}

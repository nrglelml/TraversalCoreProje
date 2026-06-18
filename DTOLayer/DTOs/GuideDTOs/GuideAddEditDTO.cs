using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.GuideDTOs
{
    public class GuideAddEditDTO
    {
        public int GuideID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string? TwitterUrl { get; set; }

        public string? InstagramUrl { get; set; }

        public bool GuideStatus { get; set; }

        public string? Image { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}

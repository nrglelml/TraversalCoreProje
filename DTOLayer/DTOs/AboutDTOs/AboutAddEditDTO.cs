using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.AboutDTOs
{
     public class AboutAddEditDTO
    {
        public int AboutID { get; set; }

        public string Title { get; set; }
        public string Title2 { get; set; }

        public string Description { get; set; }
        public string Description2 { get; set; }


        public bool AboutStatus { get; set; }

        public string? Image { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.About2DTOs
{
   public  class About2AddEditDTO
    {
        public int About2ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }


        public bool About2Status { get; set; }

        public string? Image { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}

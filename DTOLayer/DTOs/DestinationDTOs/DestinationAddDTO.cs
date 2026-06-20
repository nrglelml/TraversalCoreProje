using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.DestinationDTOs
{
    public class DestinationAddDTO
    {
        public string DestinationCity { get; set; }
        public string? DestinationDayNight { get; set; }
        public string? DestinationDescription { get; set; }
        public string? Details1 { get; set; }
        public string? Details2 { get; set; }
        public double? DestinationPrice { get; set; }
        public int? DestinationCapacity { get; set; }

        public IFormFile? DestinationImageFile { get; set; }
        public IFormFile? CoverImageFile { get; set; }
        public IFormFile? Image2File { get; set; }
    }
}

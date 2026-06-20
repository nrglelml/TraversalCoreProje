using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Guide
    {
        [Key]
        public int GuideID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [AllowNull]
        public string TwitterUrl { get; set; }
        [AllowNull]
        public string InstagramUrl { get; set; }
        public bool GuideStatus { get; set; }
        public List<Destination> Destinations { get; set; }
    }
}

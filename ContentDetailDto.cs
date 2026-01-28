using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orbitra.DTOs.Category;
using Orbitra.DTOs.City;
using Orbitra.DTOs.Category;

namespace Orbitra.DTOs.Content
{
    public class ContentDetailDto
    {
        public int ContentID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int Rank { get; set; }
        public string? Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? ImageURL { get; set; }
        public string? Website { get; set; }

        
        public CityDto City { get; set; }
        public CategoryDto Category { get; set; }
    }
}
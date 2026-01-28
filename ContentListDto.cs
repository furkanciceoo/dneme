using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbitra.DTOs.Content
{
    public class ContentListDto
    {
        public int ContentID { get; set; }
        public string Title { get; set; }
        public string? ImageURL { get; set; }
        public int Rank { get; set; }
        
        public string CategoryName { get; set; }
        public string CityName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbitra.DTOs.Category
{
    
    public class CategoryDto
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        
        public int? ParentCategoryID { get; set; }

        
        public ICollection<CategoryDto> SubCategories { get; set; }

        
        public CategoryDto()
        {
            SubCategories = new List<CategoryDto>();
        }
    }
}

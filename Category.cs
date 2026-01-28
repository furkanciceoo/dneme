using Orbitra.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orbitra.Entities.Abstract;

namespace Orbitra.Entities
{
    public class Category : IEntity
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryID { get; set; }
        public string? Description { get; set; }
        public virtual Category ParentCategory { get; set; } // Bu kategorinin üst kategorisi
        public virtual ICollection<Category> SubCategories { get; set; }

    }
}

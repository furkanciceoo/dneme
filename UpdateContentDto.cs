using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbitra.DTOs.Content
{
    public class UpdateContentDto
    {
    
    public int ContentID { get; set; } 

   
    public string Title { get; set; }

    public int CategoryID { get; set; }

    
    public int CityID { get; set; }

    public string? Description { get; set; }

   
    public string? Address { get; set; }

    
    public string? Website { get; set; }
}
}

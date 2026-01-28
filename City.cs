using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orbitra.Entities.Abstract;


namespace Orbitra.Entities 
{
    public class City : IEntity
    {
        public string Name { get; set; }
        public int CityID { get; set; }
        public int CountryID { get; set; }
        public virtual Country Country { get; set; }
    }
}

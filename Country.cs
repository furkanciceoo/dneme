using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orbitra.Entities.Abstract;


namespace Orbitra.Entities
{
    public class Country : IEntity
    {
        public int CountryID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<City> Cities { get; set; }


    }
}

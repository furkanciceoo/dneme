using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orbitra.DTOs.Country;

namespace Orbitra.DTOs.City
{
    public class CityDto
    {
        public int CityID { get; set; }
        public string Name { get; set; }

        // İlişkili Country nesnesi için ID yerine CountryDto kullanıyoruz.
        // Controller'da burayı dolduracağız.
        public CountryDto Country { get; set; }
    }
}

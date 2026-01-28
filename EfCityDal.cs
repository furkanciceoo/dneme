using Microsoft.EntityFrameworkCore;
using Orbitra.DataAccess.Abstract;
using Orbitra.DataAccess.Context;
using Orbitra.DataAccess.Repositories;
using Orbitra.Entities;

namespace Orbitra.DataAccess.EntityFramework;

public class EfCityDal : GenericRepository<City>, ICityDal
{
    // 2. Constructor:
    //    Dependency Injection ile gelen 'OrbitraContext'i alır
    //    ve temel sınıfı olan 'GenericRepository'ye yollar.
    public EfCityDal(OrbitraContext context) : base(context)
    {
        // BU KADAR!
        // İçi kasten boş bırakılır.
    }
    public List<City> GetCitiesWithCountry()
    {
        return _context.Cities
                           .Include(city => city.Country)
                           .ToList();
    }
    public List<City> GetCitiesByCountryIdWithCountry(int countryId)
    {
        // 'base' sınıftan (GenericRepository) gelen '_context'i kullanıyoruz.
        return _context.Cities                     // 1. Cities tablosuna git
                       .Include(city => city.Country) // 2. Her city için Country verisini de dahil et
                       .Where(city => city.CountryID == countryId) // 3. Sadece CountryId'si eşleşenleri filtrele
                       .ToList();
    }



    }



using Orbitra.Entities;

namespace Orbitra.DataAccess.Abstract
{
    public interface ICityDal : IGenericDal<City>
    {
       
        List<City> GetCitiesWithCountry();
        List<City> GetCitiesByCountryIdWithCountry(int countryId);
    }
}
using Orbitra.Business.Utilities.Results; // <-- Result namespace'i
using Orbitra.Entities;
using System.Linq.Expressions;

namespace Orbitra.Business.Abstract
{
    public interface ICityService
    {
        // İşlem Sonuçları (IResult)
        IResult TAdd(City entity);
        IResult TUpdate(City entity);
        IResult TDelete(City entity);

        // Veri Sonuçları (IDataResult)
        IDataResult<List<City>> TGetList();
        IDataResult<City> TGetById(int id);
        IDataResult<List<City>> TGetListByFilter(Expression<Func<City, bool>> filter);

        // Özel Metotlar
        IDataResult<List<City>> TGetCitiesWithCountry();
        IDataResult<List<City>> TGetCitiesByCountryIdWithCountry(int countryId);
    }
}
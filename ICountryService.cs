using Orbitra.Business.Utilities.Results; // <-- Result namespace'i
using Orbitra.Entities;
using System.Linq.Expressions;

namespace Orbitra.Business.Abstract
{
    public interface ICountryService
    {
        // İşlem Sonuçları
        IResult TAdd(Country entity);
        IResult TUpdate(Country entity);
        IResult TDelete(Country entity);

        // Veri Sonuçları
        IDataResult<List<Country>> TGetList();
        IDataResult<Country> TGetById(int id);
        IDataResult<List<Country>> TGetListByFilter(Expression<Func<Country, bool>> filter);
    }
}

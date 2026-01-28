using Orbitra.Business.Abstract;
using Orbitra.Business.Utilities.Results   ; // <-- Result namespace'i
using Orbitra.DataAccess.Abstract;
using Orbitra.Entities;
using System.Linq.Expressions;

namespace Orbitra.Business.Concrete
{
    public class CountryManager : ICountryService
    {
        private readonly ICountryDal _countryDal;

        public CountryManager(ICountryDal countryDal)
        {
            _countryDal = countryDal;
        }

        public IResult TAdd(Country entity)
        {
            _countryDal.Add(entity);
            return new SuccessResult("Ülke başarıyla eklendi.");
        }

        public IResult TDelete(Country entity)
        {
            _countryDal.Delete(entity);
            return new SuccessResult("Ülke silindi.");
        }

        public IResult TUpdate(Country entity)
        {
            _countryDal.Update(entity);
            return new SuccessResult("Ülke güncellendi.");
        }

        public IDataResult<Country> TGetById(int id)
        {
            var data = _countryDal.Get(c => c.CountryID == id);
            if (data == null)
            {
                return new ErrorDataResult<Country>("Ülke bulunamadı.");
            }
            return new SuccessDataResult<Country>(data, "Ülke getirildi.");
        }

        public IDataResult<List<Country>> TGetList()
        {
            var data = _countryDal.GetAll();
            return new SuccessDataResult<List<Country>>(data, "Tüm ülkeler listelendi.");
        }

        public IDataResult<List<Country>> TGetListByFilter(Expression<Func<Country, bool>> filter)
        {
            var data = _countryDal.GetAll(filter);
            return new SuccessDataResult<List<Country>>(data, "Filtrelenmiş ülkeler listelendi.");
        }
    }
}
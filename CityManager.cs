using Orbitra.Business.Abstract;
using Orbitra.Business.Utilities.Results;
using Orbitra.DataAccess.Abstract;
using Orbitra.Entities;
using System.Linq.Expressions;

namespace Orbitra.Business.Concrete
{
    public class CityManager : ICityService
    {
        private readonly ICityDal _cityDal;

        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }

        public IResult TAdd(City entity)
        {
            _cityDal.Add(entity);
            return new SuccessResult("Şehir başarıyla eklendi.");
        }

        public IResult TDelete(City entity)
        {
            _cityDal.Delete(entity);
            return new SuccessResult("Şehir silindi.");
        }

        public IResult TUpdate(City entity)
        {
            _cityDal.Update(entity);
            return new SuccessResult("Şehir güncellendi.");
        }

        public IDataResult<City> TGetById(int id)
        {
            var data = _cityDal.Get(c => c.CityID == id);
            if (data == null)
            {
                return new ErrorDataResult<City>("Şehir bulunamadı.");
            }
            return new SuccessDataResult<City>(data, "Şehir getirildi.");
        }

        public IDataResult<List<City>> TGetList()
        {
            var data = _cityDal.GetAll();
            return new SuccessDataResult<List<City>>(data, "Tüm şehirler listelendi.");
        }

        public IDataResult<List<City>> TGetListByFilter(Expression<Func<City, bool>> filter)
        {
            var data = _cityDal.GetAll(filter);
            return new SuccessDataResult<List<City>>(data, "Filtrelenmiş şehirler listelendi.");
        }

        // --- Özel Metotlar ---

        public IDataResult<List<City>> TGetCitiesWithCountry()
        {
            var data = _cityDal.GetCitiesWithCountry();
            return new SuccessDataResult<List<City>>(data, "Şehirler ve ülke bilgileri listelendi.");
        }

        public IDataResult<List<City>> TGetCitiesByCountryIdWithCountry(int countryId)
        {
            var data = _cityDal.GetCitiesByCountryIdWithCountry(countryId);
            return new SuccessDataResult<List<City>>(data, "Seçili ülkeye ait şehirler listelendi.");
        }
    }
}
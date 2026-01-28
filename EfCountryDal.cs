using Orbitra.DataAccess.Abstract;
using Orbitra.DataAccess.Context;
using Orbitra.DataAccess.Repositories;
using Orbitra.Entities;

namespace Orbitra.DataAccess.EntityFramework
{
    public class EfCountryDal : GenericRepository<Country>, ICountryDal
    {
        // 2. Constructor:
        //    Dependency Injection ile gelen 'OrbitraContext'i alır
        //    ve temel sınıfı olan 'GenericRepository'ye yollar.
        public EfCountryDal(OrbitraContext context) : base(context)
        {
            // İçi kasten boş bırakılır.
            // Bütün standart veritabanı işlemleri (Add, Get, GetAll, Update, Delete)
            // 'base' sınıftan (GenericRepository) miras alındı.
        }
    }
}
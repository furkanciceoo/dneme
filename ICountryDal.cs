using Orbitra.Entities; // Country sınıfı için

namespace Orbitra.DataAccess.Abstract
{
    // === IGenericDal<Country> MİRASINI EKLE ===
    public interface ICountryDal : IGenericDal<Country>
    {
        // Country'e özel metotlar (varsa) burada kalabilir
    }
}
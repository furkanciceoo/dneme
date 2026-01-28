using Orbitra.Entities;
using System.Collections.Generic;

namespace Orbitra.DataAccess.Abstract
{
    // === IGenericDal<Category> MİRASINI EKLE ===
    public interface ICategoryDal : IGenericDal<Category>
    {
        // Category'e özel metotlar burada kalabilir
        List<Category> GetCategoriesWithParent();
    }
}
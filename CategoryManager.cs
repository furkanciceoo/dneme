using Microsoft.Extensions.Caching.Memory;
using Orbitra.Business.Abstract;
using Orbitra.Business.Utilities.Business;
using Orbitra.Business.Utilities.Results;
using Orbitra.DataAccess.Abstract;
using Orbitra.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions; 

namespace Orbitra.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IMemoryCache _memoryCache;
        public CategoryManager(ICategoryDal categoryDal, IMemoryCache memoryCache)
        {
            _categoryDal = categoryDal;
            _memoryCache = memoryCache;
        }

        // --- EKLEME (IResult döndürüyor) ---
        public IResult TAdd(Category category)
        {
            // 1. İş Kurallarını Çalıştır
            IResult result = BusinessRules.Run(
                CheckIfCategoryNameExists(category.Name)
            // Buraya virgül koyup başka kurallar da ekleyebilirsiniz
            // Örn: CheckIfCategoryLimitExceeded()
            );

            // 2. Eğer bir kural ihlali varsa, o hatayı döndür
            if (result != null)
            {
                return result;
            }
            _categoryDal.Add(category);
            _memoryCache.Remove("Categories"); // <-- Cache'i temizle
            return new SuccessResult("Kategori eklendi.");
        }

        // --- SİLME (IResult döndürüyor) ---
        public IResult TDelete(Category category)
        {
            _categoryDal.Delete(category);
            _memoryCache.Remove("Categories");
            return new SuccessResult("Kategori silindi.");
        }

        // --- GÜNCELLEME (IResult döndürüyor) ---
        public IResult TUpdate(Category category)
        {
            _categoryDal.Update(category);
            _memoryCache.Remove("Categories");
            return new SuccessResult("Kategori güncellendi.");
        }

        // --- TEK GETİR (IDataResult döndürüyor) ---
        public IDataResult<Category> TGetById(int id)
        {
            var data = _categoryDal.Get(c => c.CategoryID == id);
            if (data == null)
            {
                return new ErrorDataResult<Category>("Kategori bulunamadı.");
            }
            return new SuccessDataResult<Category>(data, "Kategori getirildi.");
        }

        // --- LİSTELE (IDataResult döndürüyor) ---
        public IDataResult<List<Category>> TGetList()
        {
            // 1. Önce hafızada "Categories" anahtarıyla veri var mı diye bak
            if (_memoryCache.TryGetValue("Categories", out List<Category> categories))
            {
                // Varsa, veritabanına gitmeden hafızadakini döndür
                return new SuccessDataResult<List<Category>>(categories, "Kategoriler önbellekten (Cache) getirildi.");
            }

            // 2. Yoksa, veritabanından çek
            categories = _categoryDal.GetAll();

            // 3. Veriyi hafızaya kaydet (Örn: 20 dakika boyunca sakla)
            _memoryCache.Set("Categories", categories, TimeSpan.FromMinutes(20));

            return new SuccessDataResult<List<Category>>(categories, "Kategoriler listelendi ve önbelleğe alındı.");
        }

        // --- FİLTRELE (IDataResult döndürüyor) ---
        public IDataResult<List<Category>> TGetListByFilter(Expression<Func<Category, bool>> filter)
        {
            var data = _categoryDal.GetAll(filter);
            return new SuccessDataResult<List<Category>>(data, "Filtrelenmiş kategoriler listelendi.");
        }

        // --- ÖZEL METOT (IDataResult döndürüyor) ---
        public IDataResult<List<Category>> TGetCategoriesWithParent()
        {
            var data = _categoryDal.GetCategoriesWithParent();
            return new SuccessDataResult<List<Category>>(data, "Alt-üst kategoriler listelendi.");
        }


        // --- İŞ KURALLARI ---

        private IResult CheckIfCategoryNameExists(string categoryName)
        {
            // Veritabanında bu isimde bir kategori var mı?
            var result = _categoryDal.GetAll(c => c.Name == categoryName).Any();

            if (result)
            {
                return new ErrorResult("Bu isimde bir kategori zaten mevcut.");
            }
            return new SuccessResult();
        }
    }
}
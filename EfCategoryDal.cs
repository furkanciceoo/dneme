using Microsoft.EntityFrameworkCore;
using Orbitra.DataAccess.Abstract;
using Orbitra.DataAccess.Context;
using Orbitra.DataAccess.Repositories;
using Orbitra.Entities;

namespace Orbitra.DataAccess.EntityFramework;

public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
{
    // 1. Constructor: Gelen context'i base class'a (GenericRepository'ye) yollar.
    public EfCategoryDal(OrbitraContext context) : base(context)
    {
        // İçi boş kalacak.
    }

    // --- İŞTE DÜZELTİLMİŞ ÖZEL METODUNUZ ---
    public List<Category> GetCategoriesWithParent()
    {
        // 'new' VEYA 'using' KULLANILMAZ.
        // Base class'tan gelen '_context' kullanılır.

        return _context.Categories
                       .Include(c => c.ParentCategory)
                       .ToList();
    }
}
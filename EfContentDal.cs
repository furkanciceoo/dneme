using Microsoft.EntityFrameworkCore;
using Orbitra.DataAccess.Abstract;
using Orbitra.DataAccess.Context;
using Orbitra.DataAccess.Repositories;
using Orbitra.Entities;

namespace Orbitra.DataAccess.EntityFramework
{
    public class EfContentDal : GenericRepository<Content>, IContentDal
    {
        // 2. Constructor:
        //    DI ile gelen 'OrbitraContext'i alır ve
        //    temel sınıfı olan 'GenericRepository'ye yollar.
        public EfContentDal(OrbitraContext context) : base(context)
        {
            // İçi kasten boş bırakılır.
        }

        // 3. Özel Metodun Düzeltilmiş Hali:
        //    (IContentDal arayüzünüzde bu metodun imzası olmalı)
        public List<Content> GetContentsWithDetails()
        {
            // 'new OrbitraContext()' KULLANILMAZ!
            // 'base' sınıftan (GenericRepository) gelen 'protected _context'i kullanıyoruz.

            return _context.Contents
                           .Include(x => x.Category) // Kategori verisini dahil et
                           .Include(x => x.City)
                           .ThenInclude(c => c.Country)// Şehir verisini dahil et
                           .ToList();
        }
    }
}
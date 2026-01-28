using Microsoft.EntityFrameworkCore;
using Orbitra.DataAccess.Abstract;
using Orbitra.DataAccess.Context;
using System.Linq.Expressions;
using Orbitra.Entities.Abstract;
namespace Orbitra.DataAccess.Repositories
{
    // YENİ VE DOĞRU UYGULAMA
    public class GenericRepository<T> : IGenericDal<T> 
        where T : class, IEntity, new()
    {
        // protected: Miras alan EfCategoryDal gibi sınıflar da görebilsin
        protected readonly OrbitraContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(OrbitraContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // --- IGenericDal ARAYÜZÜYLE EŞLEŞEN METOTLAR ---

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        // YENİ Get metodu
        public T Get(Expression<Func<T, bool>> filter)
        {
            // FirstOrDefault kullanmak, kayıt bulunamazsa null döndürür
            return _dbSet.Where(filter).FirstOrDefault();
        }

        // YENİ GetAll metodu
        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            // Filtre null ise tüm listeyi, değilse filtrelenmiş listeyi döndür
            return filter == null
                ? _dbSet.ToList()
                : _dbSet.Where(filter).ToList();
        }
    }
}
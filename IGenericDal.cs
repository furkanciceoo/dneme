using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Orbitra.Entities.Abstract;

namespace Orbitra.DataAccess.Abstract
{
    // Bu arayüz, tüm Dal'ların uyması gereken temel kuralları belirler
    public interface IGenericDal<T> where T : class, IEntity, new()
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        // YENİ OKUMA OPERASYONLARI
        // (GetListAll ve GetListByFilter'ı birleştiren metot)
        List<T> GetAll(Expression<Func<T, bool>> filter = null);

        // (GetById(int id)'nin yerine geçen jenerik metot)
        T Get(Expression<Func<T, bool>> filter);

    }
}
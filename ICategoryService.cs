using Orbitra.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Orbitra.Business.Utilities.Results;

namespace Orbitra.Business.Abstract
{
    public interface ICategoryService
    {
        // Void -> IResult
        IResult TAdd(Category entity);
        IResult TUpdate(Category entity);
        IResult TDelete(Category entity);

        // T -> IDataResult<T>
        IDataResult<List<Category>> TGetList();
        IDataResult<Category> TGetById(int id);
        IDataResult<List<Category>> TGetListByFilter(Expression<Func<Category, bool>> filter);

        // Özel metot
        IDataResult<List<Category>> TGetCategoriesWithParent();
    }
}
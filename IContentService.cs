using Orbitra.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Orbitra.Business.Utilities.Results; 

namespace Orbitra.Business.Abstract
{
    public interface IContentService
    {
        IResult TAdd(Content entity);
        IResult TUpdate(Content entity);
        IResult TDelete(Content entity);

        // Veri dönenler -> IDataResult<T> oldu
        IDataResult<List<Content>> TGetList();
        IDataResult<Content> TGetById(int id);

        IDataResult<List<Content>> TGetListByFilter(Expression<Func<Content, bool>> filter);
        IDataResult<List<Content>> TGetContentsWithDetails();
    }
}
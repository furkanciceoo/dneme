using Orbitra.Entities;

namespace Orbitra.DataAccess.Abstract
{
    public interface IContentDal : IGenericDal<Content>
    {
        List<Content> GetContentsWithDetails();
    }
}
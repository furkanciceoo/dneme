using Orbitra.DataAccess.Abstract;
using Orbitra.Entities;

namespace Orbitra.DataAccess.Abstract
{
    public interface IUserDal : IGenericDal<User>
    {
        // Kullanıcının rollerini çekecek özel metot
        List<OperationClaim> GetClaims(User user);
    }
}
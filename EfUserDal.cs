using Orbitra.DataAccess.Abstract;
using Orbitra.DataAccess.Context;
using Orbitra.DataAccess.Repositories;
using Orbitra.Entities;
using System.Linq;


namespace Orbitra.DataAccess.EntityFramework
{
    public class EfUserDal : GenericRepository<User>, IUserDal
    {
        public EfUserDal(OrbitraContext context) : base(context)
        {
        }

        public List<OperationClaim> GetClaims(User user)
        {
            // Bu sorgu: UserOperationClaims tablosu ile OperationClaims tablosunu birleştirir
            // ve elimizdeki 'user.Id'ye ait olan rolleri çeker.
            var result = from operationClaim in _context.OperationClaims
                         join userOperationClaim in _context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.Id
                         select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

            return result.ToList();
        }
    }
   
}

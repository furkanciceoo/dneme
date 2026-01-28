using Orbitra.DataAccess.Abstract;
using Orbitra.DataAccess.Context;
using Orbitra.DataAccess.Repositories;
using Orbitra.Entities;

namespace Orbitra.DataAccess.EntityFramework
{
    public class EfUserOperationClaimDal : GenericRepository<UserOperationClaim>, IUserOperationClaimDal
    {
        public EfUserOperationClaimDal(OrbitraContext context) : base(context)
        {
        }
    }
}
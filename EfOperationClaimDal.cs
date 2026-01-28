using Orbitra.DataAccess.Abstract;
using Orbitra.DataAccess.Context;
using Orbitra.DataAccess.Repositories;
using Orbitra.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Orbitra.DataAccess.Abstract;
using Orbitra.DataAccess.Context;
using Orbitra.DataAccess.Repositories;
using Orbitra.Entities;

namespace Orbitra.DataAccess.EntityFramework
{
    // Artık 'GenericRepository' yeni 'IGenericDal'ı uyguladığı için
    // bu sınıf sözleşmeyi ihlal etmeyecektir.
    public class EfOperationClaimDal : GenericRepository<OperationClaim>, IOperationClaimDal
    {
        // Sadece 'constructor' yeterlidir.
        public EfOperationClaimDal(OrbitraContext context) : base(context)
        {
            // İçi boş kalacak
        }
    }
}
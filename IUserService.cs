using Orbitra.Business.Utilities.Results;
using Orbitra.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbitra.Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IResult Add(User user);
        IDataResult<User> GetByEmail(string email);

        // İLERİDE UPDATE VE GETBYID METOTLARI DA EKLENECEK 
    }
}

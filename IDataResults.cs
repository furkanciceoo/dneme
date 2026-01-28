using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbitra.Business.Utilities.Results
{
    // <T> ile jenerik hale getiriyoruz
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}

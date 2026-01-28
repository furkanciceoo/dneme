using Orbitra.Business.Utilities.Results;
using Orbitra.DTOs.Auth;
using Orbitra.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orbitra.Business.Utilities.Security.JWT;

namespace Orbitra.Business.Abstract
{
    public interface IAuthService
    {
        // Kayıt olma
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto);

        // Giriş yapma
        IDataResult<AccessToken> Login(UserForLoginDto userForLoginDto);

        // Yardımcı metot: Kullanıcı var mı?
        IResult UserExists(string email);

        // (Gelecekte buraya 'ChangePassword' vb. ekleyebiliriz)

        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}

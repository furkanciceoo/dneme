using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orbitra.Business.Abstract;
using Orbitra.DTOs.Auth;

namespace Orbitra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        // Business katmanındaki 'AuthManager'ı enjekte ediyoruz
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            // Başarılıysa Token'ı (ve süresini) döndür
            return Ok(userToLogin.Data);
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            // 1. Kullanıcı zaten var mı?
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            // 2. Kayıt işlemini yap
            var registerResult = _authService.Register(userForRegisterDto);
            if (!registerResult.Success)
            {
                return BadRequest(registerResult.Message);
            }

            // 3. Kayıt başarılıysa, kullanıcıya hemen bir Token üretip verelim
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}

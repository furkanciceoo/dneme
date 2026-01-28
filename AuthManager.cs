using Orbitra.Business.Abstract;
using Orbitra.Business.Utilities.Business;
using Orbitra.Business.Utilities.Results;
using Orbitra.Business.Utilities.Security.Hashing;
using Orbitra.Business.Utilities.Security.JWT;
using Orbitra.Business.Utilities.Validation;
using Orbitra.Business.ValidationRules.FluentValidation;
using Orbitra.DTOs.Auth;
using Orbitra.Entities;

namespace Orbitra.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            // --- VALIDASYON EKLENDİ ---
            // Kullanıcı verisinin doğruluğunu (email formatı, şifre uzunluğu vb.) kontrol et.
            ValidationTool.Validate(new UserForRegisterValidator(), userForRegisterDto);
            // --------------------------

            // 1. İş Kurallarını Çalıştır (Aynı mail var mı?)
            IResult ruleResult = BusinessRules.Run(
                CheckIfUserExists(userForRegisterDto.Email)
            // CheckIfEmailIsGmail(userForRegisterDto.Email) // İsterseniz bu kuralı da açabilirsiniz
            );

            // Kural hatası varsa, işlemi durdur ve hatayı döndür
            if (ruleResult != null)
            {
                return new ErrorDataResult<User>(ruleResult.Message);
            }

            // 2. Kurallar geçildiyse kayıt işlemine devam et...
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _userService.Add(user);
            return new SuccessDataResult<User>(user, "Kullanıcı başarıyla kayıt oldu.");
        }

        public IDataResult<AccessToken> Login(UserForLoginDto userForLoginDto)
        {
            // GÜNCELLEME: GetByEmail artık IDataResult dönüyor.
            // Veriye ulaşmak için '.Data' demeliyiz.
            var userToCheckResult = _userService.GetByEmail(userForLoginDto.Email);
            var userToCheck = userToCheckResult.Data;

            if (userToCheck == null)
            {
                return new ErrorDataResult<AccessToken>("Kullanıcı bulunamadı");
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<AccessToken>("Parola hatası");
            }

            // Token üretimi
            return CreateAccessToken(userToCheck);
        }

        public IResult UserExists(string email)
        {
            // GÜNCELLEME: .Data kontrolü
            var userResult = _userService.GetByEmail(email);
            if (userResult.Data != null)
            {
                return new ErrorResult("Kullanıcı mevcut");
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            // GÜNCELLEME: .Data kontrolü
            var claimsResult = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claimsResult.Data);

            return new SuccessDataResult<AccessToken>(accessToken, "Token oluşturuldu");
        }

        // --- İŞ KURALLARI ---

        // Kural 1: Aynı e-posta adresiyle kayıt olunamaz.
        private IResult CheckIfUserExists(string email)
        {
            var userExists = _userService.GetByEmail(email).Data;
            if (userExists != null)
            {
                return new ErrorResult("Bu e-posta adresiyle zaten bir kullanıcı mevcut.");
            }
            return new SuccessResult();
        }

        // Kural 2 (Senin İsteğin): Sadece Google (@gmail.com) hesaplarına izin ver.
        // Not: Bu sadece uzantıyı kontrol eder. Mailin gerçek olup olmadığını anlamak için
        // "Email Doğrulama" (aktivasyon kodu gönderme) yapmalıyız.
        private IResult CheckIfEmailIsGmail(string email)
        {
            if (!email.EndsWith("@gmail.com"))
            {
                return new ErrorResult("Sadece Google (@gmail.com) hesaplarıyla kayıt olabilirsiniz.");
            }
            return new SuccessResult();
        }
    }
}
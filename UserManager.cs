using Orbitra.Business.Abstract;
using Orbitra.Business.Utilities.Results;
using Orbitra.DataAccess.Abstract;
using Orbitra.Entities;
using Orbitra.Business.Utilities.Validation;
using Orbitra.Business.ValidationRules.FluentValidation;
namespace Orbitra.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            ValidationTool.Validate(new UserValidator(), user);
            _userDal.Add(user);
            return new SuccessResult("Kullanıcı eklendi.");
        }

        public IDataResult<User> GetByEmail(string email)
        {
            var user = _userDal.Get(u => u.Email == email);
            // Kullanıcı yoksa null döner, ama Result objesi başarılıdır (sadece data null'dır)
            // İstenirse burada "Kullanıcı bulunamadı" diye ErrorDataResult da dönülebilir.
            return new SuccessDataResult<User>(user);
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            // Artık veritabanından gerçek rolleri çekiyoruz
            var claims = _userDal.GetClaims(user);
            return new SuccessDataResult<List<OperationClaim>>(claims);
        }
    }
}


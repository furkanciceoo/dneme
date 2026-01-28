using FluentValidation;
using Orbitra.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbitra.Business.ValidationRules.FluentValidation
{
    // HATANIN ÇÖZÜMÜ BURADA:
    // Sınıfın ': AbstractValidator<UserForRegisterDto>' miras aldığından emin olun.
    public class UserForRegisterValidator : AbstractValidator<UserForRegisterDto>
    {
        public UserForRegisterValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi girin.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("İsim alanı boş olamaz.");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Soyisim alanı boş olamaz.");
        }
    }
}

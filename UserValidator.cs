using FluentValidation;
using Orbitra.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Orbitra.Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("Kullanıcı ismi boş olamaz.")
                .MinimumLength(2).WithMessage("İsim en az 2 karakter olmalıdır.");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Kullanıcı soyismi boş olamaz.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi girin.");

            // Not: Password hash/salt kontrolü yapmıyoruz çünkü onlar byte array.
            // Onların dolu olup olmadığını kontrol edebiliriz:
            RuleFor(u => u.PasswordHash).NotEmpty();
            RuleFor(u => u.PasswordSalt).NotEmpty();
        }
    }
}


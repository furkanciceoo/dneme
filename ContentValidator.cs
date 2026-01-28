using FluentValidation;
using Orbitra.Entities;

namespace Orbitra.Business.ValidationRules.FluentValidation
{
    public class ContentValidator : AbstractValidator<Content>
    {
        public ContentValidator()
        {
            // 1. Başlık Kuralları
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Başlık alanı zorunludur.") // DTO'daki [Required] yerine
                .MinimumLength(3).WithMessage("Başlık en az 3 karakter olmalıdır.") // DTO'daki [StringLength] yerine
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

            // 2. Kategori ve Şehir ID Kuralları
            RuleFor(c => c.CategoryID)
                .NotEmpty().WithMessage("Kategori ID zorunludur.")
                .GreaterThan(0).WithMessage("Geçerli bir Kategori ID'si girilmelidir."); // DTO'daki [Range] yerine

            RuleFor(c => c.CityID)
                .NotEmpty().WithMessage("Şehir ID zorunludur.")
                .GreaterThan(0).WithMessage("Geçerli bir Şehir ID'si girilmelidir.");

            // 3. Açıklama ve Adres Uzunluk Kuralları
            RuleFor(c => c.Description)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

            RuleFor(c => c.Address)
                .MaximumLength(250).WithMessage("Adres en fazla 250 karakter olabilir.");

            // 4. Web Sitesi (URL) Kuralı (Burası önemli!)
            // DTO'daki [Url] yerine burada özel bir kontrol yazıyoruz.
            RuleFor(c => c.Website)
                .Must(BeAValidUrl).When(c => !string.IsNullOrEmpty(c.Website))
                .WithMessage("Lütfen geçerli bir web sitesi URL'si girin.");
        }

        // URL Doğrulama Metodu (DTO'daki [Url] işlevini görür)
        private bool BeAValidUrl(string arg)
        {
            return Uri.TryCreate(arg, UriKind.Absolute, out _);
        }
    }
}
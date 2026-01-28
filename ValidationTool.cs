using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbitra.Business.Utilities.Validation
{
    public static class ValidationTool
    {
        // 'object' yerine 'T' kullanarak daha güvenli hale getirdik
        public static void Validate<T>(IValidator<T> validator, T entity)  where T : class
        {
            var context = new ValidationContext<T>(entity);
            var result = validator.Validate(context);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}

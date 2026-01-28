using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Orbitra.Business.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                if (e is FluentValidation.ValidationException)
                {
                    // Validasyon hatalarını 'Warning' olarak logla (Gereksiz kirliliği önler)
                    _logger.LogWarning(e, "Validasyon hatası: {Message}", e.Message);
                }
                else
                {
                    // Sistem hatalarını 'Error' olarak logla (Stack Trace dahil)
                    _logger.LogError(e, "Kritik hata: {Message}", e.Message);
                }

                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Servis hatası.";
            IEnumerable<string> errors = null;

            // Eğer hata bizim fırlattığımız bir Validasyon Hatası ise
            if (e.GetType() == typeof(ValidationException))
            {
                message = e.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest; // 400

                // Validasyon hatalarını ayıkla
                var validationException = (ValidationException)e;
                errors = validationException.Errors.Select(x => x.ErrorMessage);

                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    StatusCode = 400,
                    Message = message,
                    Errors = errors
                }.ToString());
            }

            // Diğer genel hatalar için
            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}

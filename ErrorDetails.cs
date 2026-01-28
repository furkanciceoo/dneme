using System.Text.Json;

namespace Orbitra.Business.Extensions
{
    
    public class ErrorDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

   
    public class ValidationErrorDetails : ErrorDetails
    {
        public IEnumerable<string> Errors { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orbitra.Business.Utilities.Results   ;

namespace Orbitra.Business.Utilities.Business
{
    public static class BusinessRules
    {
        // Bu metot, kendisine verilen n tane kuralı (IResult) çalıştırır.
        // Eğer bir tanesi bile başarısızsa, o başarısız sonucu döndürür.
        // Hepsi başarılıysa null döndürür.
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic; // Hata var, hatayı döndür
                }
            }
            return null; // Her şey yolunda
        }
    }
}

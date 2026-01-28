using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // <-- BU KÜTÜPHANE ŞART (Data Annotations için)
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orbitra.Entities.Abstract; // IEntity için

namespace Orbitra.Entities
{
    public class Content : IEntity
    {
        // Birincil Anahtar (Primary Key)
        public int ContentID { get; set; }

        // --- İYİLEŞTİRME 1: BAŞLIK ---
        // StringLength(200): Veritabanında nvarchar(200) olarak ayarlar.
        // Neden? Başlıklar genellikle kısadır. Arama hızı artar.
        // Required: Bu alanın veritabanında NULL olmasını engeller (Zorunlu alan).
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        // --- İYİLEŞTİRME 2: AÇIKLAMA ---
        // StringLength(2000): nvarchar(2000) yapar.
        // Açıklama uzun olabilir ama MAX (2GB) kadar da olmamalı.
        [StringLength(2000)]
        public string? Description { get; set; } // '?' işareti C# tarafında null olabilir der

        // --- İYİLEŞTİRME 3: ADRES ---
        // Adresler genelde 500 karakteri geçmez.
        [StringLength(500)]
        public string? Address { get; set; }

        // --- İYİLEŞTİRME 4: URL'LER ---
        // Web sitesi ve resim linkleri için 500 karakter yeterlidir.
        [StringLength(500)]
        public string? ImageURL { get; set; }

        [StringLength(500)]
        public string? Website { get; set; }


        // --- Diğer Alanlar (Olduğu gibi kalabilir) ---
        public int CategoryID { get; set; }
        public int CityID { get; set; }
        public int Rank { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        // --- İlişkiler (Navigation Properties) ---
        public virtual City City { get; set; }
        public virtual Category Category { get; set; }
    }
}
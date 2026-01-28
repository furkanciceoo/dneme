using System;

namespace Orbitra.DTOs.Content
{
    public class CreateContentDto
    {
        // Sadece veri taşıyan özellikler (Properties) kalacak.
        // Hiçbir [Required], [StringLength] vb. olmayacak.

        public string Title { get; set; }
        public int CategoryID { get; set; }
        public int CityID { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Website { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? ImageURL { get; set; }
    }
}
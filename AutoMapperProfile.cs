using AutoMapper;
using Orbitra.DTOs.Content; // DTO'ların namespace'i
using Orbitra.DTOs.City;     // DTO'ların namespace'i
using Orbitra.DTOs.Category;  // DTO'ların namespace'i
using Orbitra.Entities;     // Entity'lerin namespace'i

namespace Orbitra.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // --- Entity'den DTO'ya (Veri GÖNDERİRKEN) ---
            // (Bu kısım sizde zaten harikaydı, olduğu gibi kalıyor)

            CreateMap<Content, ContentListDto>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Content, ContentDetailDto>();
            CreateMap<City, CityDto>();
            CreateMap<Category, CategoryDto>();
            // Country için de ekleyebilirsiniz:
            // CreateMap<Country, CountryDto>();


            // --- DTO'dan Entity'ye (Veri ALIRKEN) ---
            // (HATAYI ÇÖZEN DÜZELTME BURADA)

            CreateMap<CreateContentDto, Content>()
                // Bu satırlar, AutoMapper'ın yeni bir 'Category' veya 'City'
                // nesnesi oluşturmasını engeller. Sadece 'CategoryID' ve 'CityID'
                // alanlarının eşleşmesine izin verir.
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.City, opt => opt.Ignore());

            CreateMap<UpdateContentDto, Content>()
                // Aynı düzeltmeyi 'Update' için de yapıyoruz
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.City, opt => opt.Ignore());
        }
    }
}
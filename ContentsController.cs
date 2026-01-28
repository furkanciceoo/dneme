using AutoMapper;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Orbitra.Business.Abstract;
using Orbitra.DTOs.Category;   
using Orbitra.DTOs.City;       
using Orbitra.DTOs.Content;     
using Orbitra.Entities;
using Orbitra.Business.Utilities.Results;

[Route("api/[controller]")]
[ApiController]
public class ContentsController : ControllerBase
{
    private readonly IContentService _contentService;
    private readonly IMapper _mapper;

    public ContentsController(IContentService contentService, IMapper mapper)
    {
        _contentService = contentService;
        _mapper = mapper;
    }
    /// <summary>
    /// Tüm içerikleri (mekanları) detaylı bir şekilde listeler.
    /// </summary>
    /// <remarks>
    /// Bu endpoint; içeriklerin başlık, kategori, şehir gibi tüm detaylarını döner.
    /// Örnek istek:
    /// 
    ///     GET /api/Contents
    ///     
    /// </remarks>
    /// <returns>İçerik listesi ve işlem sonucu.</returns>
    /// <response code="200">İşlem başarılı, liste döndü.</response>
    /// <response code="400">Bir hata oluştu.</response>
    /// <response code="401">Yetkiniz yok (Token gerekli).</response>
    [HttpGet]
    [ProducesResponseType(typeof(IDataResult<List<ContentListDto>>), 200)] // <-- Dönüş tipini belirtiyoruz
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public IActionResult GetAll()
    {
        var result = _contentService.TGetContentsWithDetails();
        if (result.Success)
        {
            // Entity Listesini -> DTO Listesine çeviriyoruz
            var dtos = _mapper.Map<List<ContentListDto>>(result.Data);

            // Sonuç olarak DTO listesini dönüyoruz
            return Ok(new SuccessDataResult<List<ContentListDto>>(dtos, result.Message));
        }
        return BadRequest(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _contentService.TGetById(id);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")] // <-- BURAYA KOYUN
    public IActionResult Create(CreateContentDto createContentDto)
    {

        var content = _mapper.Map<Content>(createContentDto);
        var result = _contentService.TAdd(content);

        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpPut]
    [Authorize(Roles = "Admin")] // <-- BURAYA KOYUN
    public IActionResult Update(UpdateContentDto updateContentDto)
    {
        // AutoMapper ile DTO'yu Entity'ye çevir
        var content = _mapper.Map<Content>(updateContentDto);

        // Business katmanındaki TUpdate metodunu çağır
        var result = _contentService.TUpdate(content);

        // İşlem başarılıysa 200 OK (ve mesajı) döndür
        if (result.Success)
        {
            return Ok(result);
        }

        // Başarısızsa 400 Bad Request (ve hata mesajını) döndür
        return BadRequest(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")] // <-- BURAYA KOYUN
    public IActionResult Delete(int id)
    {
        // Silinecek veriyi bulmak için önce ID ile getiriyoruz
        // Not: TGetById artık IDataResult dönüyor, veriye .Data ile erişiyoruz
        var contentResult = _contentService.TGetById(id);

        if (!contentResult.Success || contentResult.Data == null)
        {
            return BadRequest("Silinecek içerik bulunamadı.");
        }

        // Business katmanındaki TDelete metodunu çağır
        var result = _contentService.TDelete(contentResult.Data);

        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}
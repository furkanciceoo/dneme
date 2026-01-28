using Orbitra.Business.Abstract;
using Orbitra.Business.Utilities.Results;
using Orbitra.Business.Utilities.Validation;
using Orbitra.Business.ValidationRules.FluentValidation;
using Orbitra.DataAccess.Abstract;
using Orbitra.Entities;
using System.Linq.Expressions;

namespace Orbitra.Business.Concrete
{
    public class ContentManager : IContentService
    {
        private readonly IContentDal _contentDal;

        public ContentManager(IContentDal contentDal)
        {
            _contentDal = contentDal;
        }

        // --- EKLEME ---
        public IResult TAdd(Content entity)
        {
            ValidationTool.Validate(new ContentValidator(), entity);

            _contentDal.Add(entity);
            return new SuccessResult("İçerik başarıyla eklendi.");
        }

        // --- SİLME ---
        public IResult TDelete(Content entity)
        {
            _contentDal.Delete(entity);
            return new SuccessResult("İçerik başarıyla silindi.");
        }

        // --- GÜNCELLEME ---
        public IResult TUpdate(Content entity)
        {
            ValidationTool.Validate(new ContentValidator(), entity);

            _contentDal.Update(entity);
            return new SuccessResult("İçerik başarıyla güncellendi.");
        }

        // --- TEK GETİR ---
        public IDataResult<Content> TGetById(int id)
        {
            var data = _contentDal.Get(c => c.ContentID == id);
            if (data == null)
            {
                return new ErrorDataResult<Content>("İçerik bulunamadı.");
            }
            return new SuccessDataResult<Content>(data, "İçerik getirildi.");
        }

        // --- LİSTELE ---
        public IDataResult<List<Content>> TGetList()
        {
            return new SuccessDataResult<List<Content>>(_contentDal.GetContentsWithDetails(), "İçerikler detaylarıyla listelendi.");
        }

        // --- FİLTRELE ---
        public IDataResult<List<Content>> TGetListByFilter(Expression<Func<Content, bool>> filter)
        {
            var data = _contentDal.GetAll(filter);
            return new SuccessDataResult<List<Content>>(data, "Filtrelenmiş içerikler listelendi.");
        }

        // --- DETAYLI LİSTELE ---
        public IDataResult<List<Content>> TGetContentsWithDetails()
        {
            var data = _contentDal.GetContentsWithDetails();
            return new SuccessDataResult<List<Content>>(data, "Detaylı içerik listesi getirildi.");
        }
    }
}
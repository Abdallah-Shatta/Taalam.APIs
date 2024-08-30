using E_Learning.BL.Dtos.Category;
using E_Learning.DAL.UnitOfWorkDP;

namespace E_Learning.BL.Managers.CategoryManager
{
    public class CategoryManager : ICategoryManager
    {
        /*------------------------------------------------------------------------*/
        private readonly IUnitOfWork _unitOfWork;
        /*------------------------------------------------------------------------*/
        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /*------------------------------------------------------------------------*/
        public IEnumerable<ReadCategoryDto> GetAllCategories()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();
            //
            var _categories = categories.Select(category => new ReadCategoryDto
            {
                Id = category.Id,
                Name = category.Name
            });
            //
            return _categories;
        }
        /*------------------------------------------------------------------------*/
    }
}

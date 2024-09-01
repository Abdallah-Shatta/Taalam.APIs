using E_Learning.BL.Dtos.Category;
using E_Learning.DAL.Models;
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

        public Category createCategory(CreateCategoryDto createCategoryDto)
        {
            Category category = new Category() { Name = createCategoryDto.Name };
            _unitOfWork.CategoryRepository.Create(category);
            _unitOfWork.SaveChanges();
            return category;
        }

        public void deletecategory(int id)
        {
            Category category = _unitOfWork.CategoryRepository.GetById(id);
            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.SaveChanges();
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

        public Category updatecategory(int id, CreateCategoryDto createCategoryDto)
        {
            Category category = _unitOfWork.CategoryRepository.GetById(id);
            category.Name = createCategoryDto.Name;
            _unitOfWork.SaveChanges();
            return category;
        }
        /*------------------------------------------------------------------------*/
    }
}

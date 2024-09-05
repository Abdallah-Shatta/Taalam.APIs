using E_Learning.BL.DTO.Course;
using E_Learning.BL.Dtos.Category;
using E_Learning.BL.Managers.CourseManager;
using E_Learning.DAL.Models;
using E_Learning.DAL.UnitOfWorkDP;

namespace E_Learning.BL.Managers.CategoryManager
{
    public class CategoryManager : ICategoryManager
    {
        /*------------------------------------------------------------------------*/
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseManager _courseManager;

        /*------------------------------------------------------------------------*/
        public CategoryManager(IUnitOfWork unitOfWork, ICourseManager courseManager)
        {
            _unitOfWork = unitOfWork;
            _courseManager = courseManager;
        }
        /*------------------------------------------------------------------------*/
        //public void CreateCategory(CreateCategoryDto createCategoryDto)
        //{
        //    Category category = new Category() { Name = createCategoryDto.Name };
        //    _unitOfWork.CategoryRepository.Create(category);
        //    _unitOfWork.SaveChanges();
        //}
        /*------------------------------------------------------------------------*/
        //public void DeleteCategory(int id)
        //{
        //    Category? category = _unitOfWork.CategoryRepository.GetById(id);
        //    if (category != null)
        //    {
        //        _unitOfWork.CategoryRepository.Delete(category);
        //        _unitOfWork.SaveChanges();
        //    }
        //}
        /*------------------------------------------------------------------------*/
        public IEnumerable<ReadCategoryDto> GetAll()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();
            var _categories = categories.Select(category => new ReadCategoryDto
            {
                Id = category.Id,
                Name = category.Name
            });
            return _categories;
        }
        /*------------------------------------------------------------------------*/
        public ReadCategoryDto GetById(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetById(id);
            if(category != null)
            {
                ReadCategoryDto _category = new ReadCategoryDto()
                {
                    Id=category.Id,
                    Name = category.Name
                };
                return _category;
            }
            return null!;
        }

        /*------------------------------------------------------------------------*/
        public IEnumerable<ReadCourseDTO> GetCategoryCourses(int id)
        {
            var courses = _courseManager.GetCoursesByCategoty(id);
            if(courses != null)
            {
                return courses;
            }
            return null!;
        }
        /*------------------------------------------------------------------------*/
        //public void UpdateCategory(int id, CreateCategoryDto createCategoryDto)
        //{
        //    Category? category = _unitOfWork.CategoryRepository.GetById(id);
        //    if (category != null)
        //    {
        //        category.Name = createCategoryDto.Name;
        //        _unitOfWork.SaveChanges();
        //    }
        //}
    }
}

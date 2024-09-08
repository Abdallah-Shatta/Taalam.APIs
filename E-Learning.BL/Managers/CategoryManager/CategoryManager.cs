using E_Learning.BL.DTO.Category;
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
        /*------------------------------------------------------------------------*/
        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
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
        public CategoryWithCoursesDTO GetCategoryWithCourses(int id)
        {
            var categoy = _unitOfWork.CategoryRepository.GetByIdWithCourses(id);
            if(categoy != null)
            {
                return new CategoryWithCoursesDTO()
                {
                    Id = categoy.Id,
                    Name = categoy.Name,
                    Courses = categoy.Courses.Select(c => new ReadCourseDTO()
                    {
                        Id = c.Id,
                        Title = c.Title,
                        InstructorName = c.User.FName + " " + c.User.LName,
                        Description = c.Description,
                        Price = c.Price,
                        Rate = c.Rate,
                        CoverPicture = c.CoverPicture,
                        CategoryName = c.Category.Name
                    }).ToList()
                };
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
    }
}

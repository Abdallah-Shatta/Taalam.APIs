using E_Learning.BL.DTO.Category;
using E_Learning.BL.DTO.Course;
using E_Learning.BL.Dtos.Category;

namespace E_Learning.BL.Managers.CategoryManager
{
    public interface ICategoryManager
    {
        IEnumerable<ReadCategoryDto> GetAll();
        ReadCategoryDto GetById(int id);
        CategoryWithCoursesDTO GetCategoryWithCourses(int id);
        //void CreateCategory(CreateCategoryDto createCategoryDto);
        //void UpdateCategory(int id, CreateCategoryDto createCategoryDto);
        //void DeleteCategory(int id);
    }
}

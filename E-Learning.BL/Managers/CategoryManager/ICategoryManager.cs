using E_Learning.BL.Dtos.Category;
using E_Learning.DAL.Models;

namespace E_Learning.BL.Managers.CategoryManager
{
    public interface ICategoryManager
    {
        IEnumerable<ReadCategoryDto> GetAllCategories();

        Category createCategory(CreateCategoryDto createCategoryDto);

        Category updatecategory(int id, CreateCategoryDto createCategoryDto);

        void deletecategory(int id);
    }
}

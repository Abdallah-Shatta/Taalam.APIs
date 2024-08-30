using E_Learning.BL.Dtos.Category;

namespace E_Learning.BL.Managers.CategoryManager
{
    public interface ICategoryManager
    {
        IEnumerable<ReadCategoryDto> GetAllCategories();
    }
}

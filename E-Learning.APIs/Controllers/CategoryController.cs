using E_Learning.BL.Dtos.Category;
using E_Learning.BL.Managers.CategoryManager;
using E_Learning.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.APIs.Controllers
{

    public class CategoryController :APIBaseController
    {
        /*------------------------------------------------------------------------*/
        private readonly ICategoryManager _categoryManager;
        /*------------------------------------------------------------------------*/
        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
        /*------------------------------------------------------------------------*/
        [HttpGet]
        public ActionResult<IEnumerable<ReadCategoryDto>> GetAll()
        {
            var categories = _categoryManager.GetAllCategories();
            return Ok(categories);
        }
        /*------------------------------------------------------------------------*/

        [HttpPost]
        public ActionResult<Category> createCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            return Ok( _categoryManager.createCategory(createCategoryDto) );
        }

        [HttpPut]
        public ActionResult<Category> updatecategory(int id,[FromBody] CreateCategoryDto createCategoryDto)
        {
            
            return Ok(_categoryManager.updatecategory(id, createCategoryDto));
        }

        [HttpDelete]
        public void deleteCategory(int id)
        {
            _categoryManager.deletecategory(id);
        }
        
    }
}

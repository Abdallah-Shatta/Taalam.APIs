using E_Learning.BL.DTO.User;
using E_Learning.BL.Dtos.Category;
using E_Learning.BL.Managers.CategoryManager;
using E_Learning.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace E_Learning.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        /*------------------------------------------------------------------------*/
        private readonly ICategoryManager _categoryManager;
        /*------------------------------------------------------------------------*/
        public CategoriesController(ICategoryManager categoryManager)
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

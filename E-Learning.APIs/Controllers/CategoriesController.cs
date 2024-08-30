using E_Learning.BL.Dtos.Category;
using E_Learning.BL.Managers.CategoryManager;
using Microsoft.AspNetCore.Mvc;

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
    }
}

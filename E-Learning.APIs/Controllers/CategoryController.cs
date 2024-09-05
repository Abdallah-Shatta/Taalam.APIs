using E_Learning.BL.DTO.Course;
using E_Learning.BL.Dtos.Category;
using E_Learning.BL.Managers.CategoryManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.APIs.Controllers
{

    [Authorize]
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
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<ReadCategoryDto>> GetAll()
        {
            var categories = _categoryManager.GetAll();
            return Ok(categories);
        }
        /*------------------------------------------------------------------------*/
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ReadCourseDTO>> GetCoursesByCategoryId(int id)
        {
            var courses = _categoryManager.GetCategoryCourses(id);
            if(courses == null || !courses.Any())
            {
                return NotFound(new { Message = "Category Courses Not Found" });
            }
            return Ok(courses);
        }
        /*------------------------------------------------------------------------*/
        //[HttpPost]
        //public ActionResult CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        //{
        //    if(createCategoryDto != null)
        //    {
        //        _categoryManager.CreateCategory(createCategoryDto);
        //        return Ok(new { Message = "Category Creation Succeded" });
        //    }
        //    return BadRequest(ModelState);
        //}

        /*------------------------------------------------------------------------*/
        //[HttpPut]
        //public ActionResult<Category> UpdateCategory(int id,[FromBody] CreateCategoryDto createCategoryDto)
        //{
        //    if(createCategoryDto != null)
        //    {
        //        _categoryManager.UpdateCategory(id, createCategoryDto);
        //        return Ok();
        //    }
        //    return BadRequest(ModelState);
        //}

        /*------------------------------------------------------------------------*/
        //[HttpDelete]
        //public void DeleteCategory(int id)
        //{
        //    _categoryManager.DeleteCategory(id);
        //}
        
    }
}

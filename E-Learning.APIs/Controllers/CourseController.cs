
using E_Learning.BL.Managers.CourseManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.APIs.Controllers
{
    [Authorize]
    public class CourseController : APIBaseController
    {
        private readonly ICourseManager _courseManager;
        public CourseController(ICourseManager courseManager)
        {
            this._courseManager = courseManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult getCourseDetailsById(int id)
        {
            var course = _courseManager.GetCourseDetailsById(id);
            if (course == null) {
                return NotFound();
            }
            return Ok(course);
        }
    }
}

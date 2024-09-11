using E_Learning.BL.DTO.CourseDTO.CourseUploadDTO;
using E_Learning.BL.DTO.User;
using E_Learning.BL.Managers.CourseManager;
using E_Learning.DAL.Models;
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

        [HttpGet("content/{courseId}")]
        [AllowAnonymous]
        public IActionResult getCourseContentForUser([FromQuery] int userId, int courseId)

        {
            if (_courseManager.IsStudentEnrolled(userId, courseId)==false){
                return BadRequest();
            }
            

            var course = _courseManager.GetCourseContentForUser(userId, courseId);
            if (course == null)
            {
                return NoContent();
            }
            return Ok(course);
        }


        /////////////////////////////////////////////////////////////////////////////
        [AllowAnonymous]

        [HttpGet("GetAllCourses")]
        public ActionResult<IEnumerable<CourseDTO>> GetAllCourses()
        {
            var courses = _courseManager.GetAllCourses();
            return Ok(courses);
        }

        [AllowAnonymous]

        [HttpGet("SearchCourses")]
        public ActionResult<IEnumerable<CourseDTO>> SearchCourses(string searchTerm)
        {
            var courses = _courseManager.SearchCourses(searchTerm);
            return Ok(courses);
        }

        [HttpPost("uploadCourse")]
        [AllowAnonymous]
        public ActionResult UploadCourse(UploadCourseDTO courseDto)
        {
            var result = _courseManager.UploadCourse(courseDto);
            if (result.success)
            {
                return Ok(result.message);
            }
            else
            {
                return BadRequest(result.message);
            }
        }
    }
}

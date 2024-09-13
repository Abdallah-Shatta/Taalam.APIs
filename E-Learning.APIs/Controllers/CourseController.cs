using E_Learning.BL.DTO.Course;
using E_Learning.BL.DTO.CourseDTO.CourseUploadDTO;

using E_Learning.BL.DTO.CourseDTO.CourseSectionInfoDTO.CourseLessonDTO;
using E_Learning.BL.DTO.CourseDTO.EnrollmentDTO;

﻿using E_Learning.BL.DTO.CourseDTO.CourseUploadDTO;
using E_Learning.BL.DTO.User;
using E_Learning.BL.Managers.CourseManager;
using E_Learning.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public IActionResult getCourseContentForUser(int courseId)

        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(); // Return 401 Unauthorized if the user is not logged in
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }

            if (_courseManager.IsStudentEnrolled(userId, courseId)==false){
                return BadRequest("user is not enrolled in this course");
            }
            

            var course = _courseManager.GetCourseContentForUser(userId, courseId);
            if (course == null)
            {
                return NoContent();
            }
            return Ok(course);
        }

        [HttpPost("enroll")]
        public IActionResult EnrollInCourse([FromBody] EnrollPostRequestDTO enrollRequest)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(); // Return 401 Unauthorized if the user is not logged in
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized(); 
            }
            if (_courseManager.IsStudentEnrolled(userId, enrollRequest.CourseId))
            {
                return BadRequest("User is already enrolled in this course.");
            }

            var enrollmentResult = _courseManager.EnrollUserInCourse(userId, enrollRequest.CourseId);

            if (enrollmentResult)
            {
                return Ok(new { message = "User enrolled successfully!" });
            }

            return StatusCode(500, "There was an error enrolling the user.");
        }


        [HttpPost("complete-lesson")]
        public  IActionResult CompleteLesson([FromBody] CompleteLessonRequestDTO request)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(); 
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }


            if (_courseManager.IsStudentEnrolled(userId, request.CourseId)==false)
            {
                return BadRequest("User is not enrolled in course.");
            }

            var result =  _courseManager.CompleteLesson(userId, request.CourseId, request.LessonId);
            if (!result)
            {
                return BadRequest("Failed to mark lesson as completed.");
            }

            return Ok(new { message = "Lesson marked as completed" });
        }


        [HttpGet("debuguser")]
        public IActionResult debugUser()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(claims);
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
            (var success, var message) = _courseManager.UploadCourse(courseDto);
            if (success == true)
            {
                return Ok(new { message = message });
            }
            else
            {
                return BadRequest(new { message = message });
            }
        }

        [HttpGet("GetAllUserCourses/{userId}")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<EnrolledCourse>> GetAllUserCourses(int userId)
        {
            var courses = _courseManager.GetCoursesByUserId(userId);
            if (courses.Any())
            {
                return Ok(courses);
            }
            return NotFound("No courses found for this user.");
        }
    }
}

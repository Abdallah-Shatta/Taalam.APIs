using E_Learning.BL.DTO.Course;
using E_Learning.BL.DTO.CourseDTO.CourseContentDTO;
using E_Learning.BL.DTO.CourseDTO.CourseUploadDTO;
using E_Learning.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.BL.Managers.CourseManager
{
    public interface ICourseManager
    {
        ReadOneCourseDetailsDto GetCourseDetailsById(int id);
        ReadCourseContentDTO GetCourseContentForUser(int userId, int courseId);

        IEnumerable<ReadCourseDTO> GetAllCourses();
        IEnumerable<ReadCourseDTO> SearchCourses(string searchTerm);
        IEnumerable<ReadCourseDTO> GetCoursesByCategoty(int id);
        bool IsStudentEnrolled(int userId, int courseId);

        (bool success, string message) UploadCourse(UploadCourseDTO uploadCourse);
    }
}

using E_Learning.DAL.Models;
using E_Learning.DAL.Repositories.GenericRepository;

namespace E_Learning.DAL.Repositories.CourseRepository
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Course getCourseDetailsById(int id);
        (Course, Enrollment) GetCourseContentForUser(int userId, int courseId);

        IEnumerable<Course> GetAllCourses();
        IEnumerable<Course> SearchCourses(string searchTerm);
        IEnumerable<Course> GetAllCoursesByUserId(int id);
    }
}

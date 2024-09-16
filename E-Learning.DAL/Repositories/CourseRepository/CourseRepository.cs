using E_Learning.DAL.Data.Context;
using E_Learning.DAL.Models;
using E_Learning.DAL.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.DAL.Repositories.CourseRepository
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context)
        {
        }

        public Course getCourseDetailsById(int id)
        {
            var course = _context.Courses
                .Include(c => c.Sections)
                    .ThenInclude(s => s.Lessons)
                .Include(c => c.Sections)
                    .ThenInclude(s => s.Quizes)
                .Include(c => c.Category)
                .Include(c => c.User)
                .FirstOrDefault(c => c.Id == id);

            return course;
        }

        public (Course, Enrollment) GetCourseContentForUser(int userId, int courseId)
        {
            var course = _context.Courses
                .Include(c => c.Sections)
                    .ThenInclude(s => s.Lessons)
                .Include(c => c.Sections)
                    .ThenInclude(s => s.Quizes)
                .FirstOrDefault(c => c.Id == courseId);

            var enrollment = _context.Enrollments
                 .Include(e => e.CompletedLessonsList)
                .FirstOrDefault(e => e.CourseId == courseId && e.UserId == userId);

            if (course == null || enrollment == null)
            {
                return (null, null);
            }

            return (course, enrollment);

        }

        public HashSet<int> GetCompletedLessonIdsForUserAndCourse(int userId, int courseId)
        {
            return _context.CompletedLessons
                .Where(cl => cl.UserId == userId && cl.CourseId == courseId)
                .Select(cl => cl.LessonId)
                .ToHashSet();
        }

        
        public void CreateCertificate(int userId, int courseId)
        {
            
           var newCert = _context.CertificatesOfCompletion.Add(new CertificateOfCompletion
            {
                UserId = userId,
                CourseId = courseId
            });  

            


        }

        public CertificateOfCompletion? GetCertOfComp(int userId, int courseId)
        {
            return _context.CertificatesOfCompletion
               .Include(c => c.User)   
               .Include(c => c.Course) 
               .FirstOrDefault(c => c.UserId == userId && c.CourseId == courseId);
        }

        public bool CertAlreadyExists(int userId, int courseId)
        {
            return _context.CertificatesOfCompletion.Any(c => c.UserId == userId && c.CourseId == courseId);

            
        }






        /////////////////////////////////////////////////////////////
        public IEnumerable<Course> GetAllCourses()
        {
            return _context.Courses.Include(c => c.User).Include(c => c.Category);
        }

        public IEnumerable<Course> SearchCourses(string searchTerm)
        {
            return _context.Courses.Include(c => c.User).Include(c => c.Category)
           .Where(c => c.Title.Contains(searchTerm) || c.Description.Contains(searchTerm));
        }
        public IEnumerable<Course> GetAllCoursesByUserId(int userId)
        {
            return _context.Courses
                  .Include(c => c.Enrollments)
                  .Include(c => c.User)
                  .Include(c => c.Category)
                  .Where(c => c.Enrollments.Any(e => e.UserId == userId))
                  .ToList();
        }
    }
}

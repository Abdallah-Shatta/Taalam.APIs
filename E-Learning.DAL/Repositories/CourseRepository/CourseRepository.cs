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
            var course= _context.Courses
                .Include(c => c.Sections)
                    .ThenInclude(s => s.Lessons)
                .Include(c => c.Sections)
                    .ThenInclude(s => s.Quizes)
                .Include(c => c.Category) 
                .Include(c => c.User) 
                .FirstOrDefault(c => c.Id == id);

            return course;
        }

        /////////////////////////////////////////////////////////////
        public IEnumerable<Course> GetAllCourses()
        {
            return _context.Courses.Include(c => c.User).Include(c => c.Category);
        }

        public IEnumerable<Course> SearchCourses(string searchTerm)
        {
            return _context.Courses.Include(c => c.User)
           .Where(c => c.Title.Contains(searchTerm) || c.Description.Contains(searchTerm));
        }

    }
}

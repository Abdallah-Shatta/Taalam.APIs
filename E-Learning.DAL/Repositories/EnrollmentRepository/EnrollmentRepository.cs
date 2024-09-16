using E_Learning.DAL.Data.Context;
using E_Learning.DAL.Models;
using E_Learning.DAL.Repositories.GenericRepository;

namespace E_Learning.DAL.Repositories.EnrollmentRepository
{
    public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(AppDbContext _context) : base(_context)
        {
        }

        public bool IsStudentEnrolled(int userId, int courseId)
        {
            var isEnrolled = _context.Enrollments.Any(e => e.UserId == userId && e.CourseId == courseId); ;

            

            return isEnrolled;
        }

        public Enrollment? GetEnrollment(int userId, int courseId)
        {
            var enrollment = _context.Enrollments.Where(e => e.UserId == userId).Where(e => e.CourseId == courseId).FirstOrDefault();

            if (enrollment == null)
            {
                return null;
            }

            return enrollment;
        }


        public void AddEnrollment(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
        }

        public bool IsStudentEnrolled(int userId)
        {
            return _context.Enrollments.Any(e => e.UserId == userId);
        }

       public void UpdateEnrollment(Enrollment enrollment)
        {
            _context.Enrollments.Update(enrollment);

        }

        public bool IsEnrollmentComplete(int userId, int courseId)
        {
            return _context.Enrollments.Any(e => e.UserId == userId&&e.CourseId==courseId&&e.ProgressPercentage==100);
        }

        


       


    }
}

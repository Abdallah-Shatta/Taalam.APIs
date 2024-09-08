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
            var enrollment = _context.Enrollments.Where(e=>e.UserId==userId).Where(e=>e.CourseId==courseId).FirstOrDefault();

            if (enrollment != null) {
                return true;
            }

            return false;
        }

    }
}

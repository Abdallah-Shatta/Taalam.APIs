using E_Learning.BL.DTO.User;
using E_Learning.DAL.UnitOfWorkDP;

namespace E_Learning.BL.Managers.CategoryManager
{
    public class UserManager : IUserManager
    {
        /*------------------------------------------------------------------------*/
        private readonly IUnitOfWork _unitOfWork;
        /*------------------------------------------------------------------------*/
        public UserManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public InstructorDTO GetInstructorInfo(int id)
        {

            var userFromDb = _unitOfWork.UserRepository.GetInstructorInfo(id);
            if (userFromDb == null)
                return null;
            var totalCourses = userFromDb.OwnedCourses;
            var totalStudents = 0;
            foreach (var course in totalCourses)
            {
                totalStudents += _unitOfWork.UserRepository.CountEnrollment(course.Id);
            }

            InstructorDTO InstructorInfo = new InstructorDTO
            {
                FName = userFromDb.FName,
                LName = userFromDb.LName,
                Facebook = userFromDb.Facebook,
                Linkedin = userFromDb.Linkedin,
                Youtube = userFromDb.Youtube,
                Twitter = userFromDb.Twitter,
                Description = userFromDb.Description,
                ProfilePicture = userFromDb.ProfilePicture,
                TotalStudents = totalStudents,
                OwnedCourses = userFromDb.OwnedCourses.Select(c => new CourseDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    CoverPicture = c.CoverPicture,
                    Description = c.Description,
                    Price = c.Price,
                    Rate = c.Rate
                }).ToList()
            };
            return InstructorInfo;
        }
    }
}

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
        public bool EditUserProfile(EditUserProfileDTO editUserProfileDTO)
        {
            var userFromDb = _unitOfWork.UserRepository.GetInstructorInfo(editUserProfileDTO.Id);
            if (userFromDb == null)
                return false;

            userFromDb.FName = editUserProfileDTO.FName;
            userFromDb.LName = editUserProfileDTO.LName;
            userFromDb.Description = editUserProfileDTO.Description;
            userFromDb.GitHub = editUserProfileDTO.GitHub;
            userFromDb.Twitter = editUserProfileDTO.Twitter;
            userFromDb.Facebook = editUserProfileDTO.Facebook;
            userFromDb.Linkedin = editUserProfileDTO.LinkedIn;
            userFromDb.Youtube = editUserProfileDTO.Youtube;
            userFromDb.ProfilePicture = editUserProfileDTO.ProfilePicture;
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}

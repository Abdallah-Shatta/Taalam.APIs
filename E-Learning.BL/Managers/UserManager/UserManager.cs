using Azure.Core;
using E_Learning.BL.DTO.User;
using E_Learning.DAL.UnitOfWorkDP;
using Microsoft.AspNetCore.Hosting;

namespace E_Learning.BL.Managers.CategoryManager
{
    public class UserManager : IUserManager
    {
        /*------------------------------------------------------------------------*/
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env; // To get access to the "uploads" folder path
        /*------------------------------------------------------------------------*/
        public UserManager(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
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
                Github = userFromDb.GitHub,
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
        public async Task<bool> EditUserProfile(EditUserProfileDTO editUserProfileDTO, string scheme, string host)
        {
            var userFromDb = _unitOfWork.UserRepository.GetById(editUserProfileDTO.Id);
            if (userFromDb == null)
                return false;
            userFromDb.FName = editUserProfileDTO.FName;
            userFromDb.LName = editUserProfileDTO.LName;
            userFromDb.Description = editUserProfileDTO.Description;
            userFromDb.GitHub = editUserProfileDTO.Github;
            userFromDb.Twitter = editUserProfileDTO.Twitter;
            userFromDb.Facebook = editUserProfileDTO.Facebook;
            userFromDb.Linkedin = editUserProfileDTO.LinkedIn;
            userFromDb.Youtube = editUserProfileDTO.Youtube;
            if (editUserProfileDTO.ProfilePictureFile != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(editUserProfileDTO.ProfilePictureFile.FileName);
                var filePath = Path.Combine(_env.WebRootPath, "uploads", fileName); // Save to wwwroot/uploads
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await editUserProfileDTO.ProfilePictureFile.CopyToAsync(stream); // Save the file to the server
                }
                userFromDb.ProfilePicture = $"{scheme}://{host}/uploads/{fileName}";
            }
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}

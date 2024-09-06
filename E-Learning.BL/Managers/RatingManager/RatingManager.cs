using E_Learning.BL.DTO.CourseDTO;
using E_Learning.BL.DTO.CourseDTO.CourseRatingDTO;
using E_Learning.BL.DTO.User;
using E_Learning.DAL.UnitOfWorkDP;

namespace E_Learning.BL.Managers.RatingManager
{
    public class RatingManager: IRatingManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public RatingManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


       public IEnumerable<ReadCourseRatingByUserDTO> GetAllRatingGivenByUser(int userId)
        {
            var ratings = _unitOfWork.RatingRepository.GetAllRatingGivenByUser(userId).Select(rating => new ReadCourseRatingByUserDTO
            {
                Id = rating.Id,
                Description = rating.Description,
                Value = rating.Value,
                Course = rating.Course == null ? null : new ReadCourseInfoForRatingDTO
                {
                    Id = rating.Course.Id,
                    Title = rating.Course.Title,
                    Rate = rating.Course.Rate,

                }
            }).ToList();


            return ratings;
        }


        public IEnumerable<ReadCourseRatingDTO> GetAllRatingsForCourse(int courseId)
        {
            var ratings = _unitOfWork.RatingRepository.GetAllRatingsForCourse(courseId).Select(rating => new ReadCourseRatingDTO
            {
                Id = rating.Id,
                Description = rating.Description,
                Value = rating.Value,

                Student = rating.User==null ? null : new ReadUserInfoForRating()
                {
                    Id= rating.User.Id,
                    ProfilePicture = rating.User.ProfilePicture ?? null,
                    Name = rating.User.FName+" "+rating.User.LName

                }

            }).ToList();


            return ratings;
        }

    }
}

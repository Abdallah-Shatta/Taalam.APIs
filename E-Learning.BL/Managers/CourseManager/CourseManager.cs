using E_Learning.BL.DTO.Course;
using E_Learning.BL.DTO.CourseDTO.CourseContentDTO;
using E_Learning.BL.DTO.CourseDTO.CourseSectionDTO;
using E_Learning.BL.DTO.CourseDTO.CourseSectionInfoDTO.CourseLessonDTO;
using E_Learning.BL.DTO.CourseDTO.CourseSectionInfoDTO.CourseQuizInfoDTO;
using E_Learning.BL.DTO.CourseDTO.InstructorInfoDTO;
using E_Learning.DAL.Models;
using E_Learning.DAL.UnitOfWorkDP;
using Microsoft.AspNetCore.Authorization;

namespace E_Learning.BL.Managers.CourseManager
{
    public class CourseManager : ICourseManager
    {

        private readonly IUnitOfWork _unitOfWork;

        public CourseManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ReadOneCourseDetailsDto GetCourseDetailsById(int id)
        {
            var course = _unitOfWork.CourseRepository.getCourseDetailsById(id);
            if (course == null)
            {
                return null;
            }


            ReadOneCourseDetailsDto courseDetails = new ReadOneCourseDetailsDto
            {
                Id = course.Id,
                Instructor = new ReadCourseInstructorInfoDTO
                {
                    Id = course.User.Id,
                    FName = course.User.FName,
                    LName = course.User.LName,
                    Description = course.User.Description

                },

                CourseCategory = course.Category == null ? null : new Dtos.Category.ReadCategoryDto
                {
                    Id = course.Category.Id,
                    Name = course.Category.Name,
                },

                Title = course.Title,
                Description = course.Description,
                CoverPicture = course.CoverPicture,
                Price = course.Price,
                Rate = course.Rate,
                CreationDate = course.CreationDate,
                UpdatedDate = course.UpdatedDate,
                SectionsNo = course.Sections != null ? course.Sections.Count() : 0,

                Sections = course.Sections == null ? null : course.Sections.Select(section => new ReadCourseSectionInfoDTO
                {
                    Id = section.Id,
                    SectionNumber = section.SectionNumber,
                    Title = section.Title,
                    LessonsNo = section.Lessons != null ? section.Lessons.Count() : 0,

                    Lessons = section.Lessons == null ? null : section.Lessons.Select(lesson => new ReadCourseLessonDTO

                    {
                        Id = lesson.Id,
                        Title = lesson.Title,
                        Duration = lesson.Duration,

                    }).ToList(),
                    Quizes = section.Quizes == null ? null : section.Quizes.Select(quiz => new ReadCourseQuizInfoDTO
                    {
                        Id = quiz.Id,
                        Title = quiz.Title,


                    }).ToList(),
                }).ToList()
            };
            return courseDetails;
        }

        public ReadCourseContentDTO GetCourseContentForUser(int userId, int courseId)
        {

            (var course, var ennrollment) = _unitOfWork.CourseRepository.GetCourseContentForUser(userId, courseId);


            ReadCourseContentDTO couresResult = new ReadCourseContentDTO
            {
                Id = course.Id,
                TeacherId = course.UserId,
                Duration = course.Duration,

                Sections = course.Sections == null ? null : course.Sections.Select(section => new ReadCourseSectionInfoDTO
                {
                    Id = section.Id,
                    SectionNumber = section.SectionNumber,
                    Title = section.Title,
                    LessonsNo = section.Lessons != null ? section.Lessons.Count() : 0,

                    Lessons = section.Lessons == null ? null : section.Lessons.Select(lesson => new ReadCourseLessonDTO

                    {
                        Id = lesson.Id,
                        Title = lesson.Title,
                        Duration = lesson.Duration,
                        Content = lesson.Content,

                    }).ToList(),
                    Quizes = section.Quizes == null ? null : section.Quizes.Select(quiz => new ReadCourseQuizInfoDTO
                    {
                        Id = quiz.Id,
                        Title = quiz.Title,


                    }).ToList(),
                }).ToList(),

                StudentEnnrollment = new CourseEnrollmentInfoDTO
                {
                    ProgressPercentage = ennrollment.ProgressPercentage,
                    CompletedLessons = ennrollment.CompletedLessons,
                    EnrollmentDate = ennrollment.EnrollmentDate,
                }




            };

            return couresResult;


        }

        public bool IsStudentEnrolled(int userId, int courseId)
        {
            return _unitOfWork.EnrollmentRepository.IsStudentEnrolled(userId, courseId);
        }

        ////////////////////////////////////////////////////////////////////////////////
        public IEnumerable<ReadCourseDTO> GetAllCourses()
        {

            return _unitOfWork.CourseRepository.GetAllCourses()
                .Select(c => new ReadCourseDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    InstructorName = c.User.FName + " " + c.User.LName,
                    Description = c.Description,
                    Price = c.Price,
                    Rate = c.Rate,
                    CoverPicture = c.CoverPicture,
                    CategoryName = c.Category.Name,
                    Duration = c.Duration,
                });
        }

        public IEnumerable<ReadCourseDTO> SearchCourses(string searchTerm)
        {
            return _unitOfWork.CourseRepository.SearchCourses(searchTerm)
                .Select(c => new ReadCourseDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    InstructorName = c.User?.FName + " " + c.User?.LName,
                    Description = c.Description,
                    Price = c.Price,
                    Rate = c.Rate,
                    CoverPicture = c.CoverPicture,
                    CategoryName = c.Category?.Name,
                    Duration = c.Duration,

                });
        }

        public IEnumerable<ReadCourseDTO> GetCoursesByCategoty(int id)
        {
            return _unitOfWork.CourseRepository.GetAllCourses().Where(c => c.CategoryId == id)
                .Select(c => new ReadCourseDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    InstructorName = c.User.FName + " " + c.User.LName,
                    Description = c.Description,
                    Price = c.Price,
                    Rate = c.Rate,
                    CoverPicture = c.CoverPicture,
                    CategoryName = c.Category.Name,
                    Duration = c.Duration,

                });
        }
    }
}

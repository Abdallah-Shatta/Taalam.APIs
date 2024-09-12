using E_Learning.BL.DTO.Course;
using E_Learning.BL.DTO.CourseDTO.CourseContentDTO;
using E_Learning.BL.DTO.CourseDTO.CourseSectionDTO;
using E_Learning.BL.DTO.CourseDTO.CourseSectionInfoDTO.CourseLessonDTO;
using E_Learning.BL.DTO.CourseDTO.CourseSectionInfoDTO.CourseQuizInfoDTO;
using E_Learning.BL.DTO.CourseDTO.CourseUploadDTO;
using E_Learning.BL.DTO.CourseDTO.InstructorInfoDTO;
using E_Learning.BL.DTO.User;
using E_Learning.DAL.Models;
using E_Learning.DAL.UnitOfWorkDP;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

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

            var completedLessonIds = _unitOfWork.CourseRepository.GetCompletedLessonIdsForUserAndCourse(userId, courseId);
            int totalLessons = course.Sections?.Sum(section => section.Lessons.Count) ?? 0;
            int completedLessonsCount = completedLessonIds.Count;

            decimal progressPercentage = totalLessons > 0 ? (decimal)completedLessonsCount / totalLessons * 100 : 0;
            ReadCourseContentDTO couresResult = new ReadCourseContentDTO
            {
                Id = course.Id,
                TeacherId = course.UserId,
                Duration = course.Duration,

                Sections = course.Sections?.Select(section => new ReadCourseSectionInfoDTO
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
                        IsCompleted = completedLessonIds.Contains(lesson.Id)

                    }).ToList(),
                    Quizes = section.Quizes == null ? null : section.Quizes.Select(quiz => new ReadCourseQuizInfoDTO
                    {
                        Id = quiz.Id,
                        Title = quiz.Title,

                    }).ToList()
                }).ToList(),

                StudentEnrollment = new CourseEnrollmentInfoDTO
                {
                    ProgressPercentage = progressPercentage,
                    CompletedLessons = completedLessonsCount,
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

        public bool EnrollUserInCourse(int userId, int courseId)
        {
            //var existingEnrollment = _unitOfWork.EnrollmentRepository.IsStudentEnrolled(userId,courseId);

            //if (existingEnrollment)
            //{
            //    return false; 
            //}

            var enrollment = new Enrollment
            {
                UserId = userId,
                CourseId = courseId,
                EnrollmentDate = DateTime.UtcNow,
                ProgressPercentage = 0,
                CompletedLessons = 0
            };

            _unitOfWork.EnrollmentRepository.AddEnrollment(enrollment);
            _unitOfWork.SaveChanges();
            return true;
        }


        public  bool CompleteLesson(int userId, int courseId, int lessonId)
        {

            var enrollment =  _unitOfWork.EnrollmentRepository.GetEnrollment(userId, courseId);
            if (enrollment == null) return false;

            var completedLesson = new CompletedLesson
            {
                UserId = userId,
                LessonId = lessonId,
                CourseId = courseId,
                CompletedDate = DateTime.Now
            };

            _unitOfWork.LessonRepository.MarkLessonAsComplete(userId, lessonId, courseId);

            //// Update progress
            //var totalLessons = _unitOfWork.LessonRepository.GetTotalLessons(courseId);
            //var completedLessonsCount = _unitOfWork.CompletedLessonRepository.GetCompletedLessonsCount(userId, courseId);
            //var progressPercentage = (decimal)completedLessonsCount / totalLessons * 100;

            _unitOfWork.SaveChanges();
            return true;

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

        public (bool success , string message) UploadCourse(UploadCourseDTO uploadCourse)
        {
            #region trying
            try
            {
                Course uploadedCourse = new Course
                {
                    UserId = uploadCourse.UserId,
                    Title = uploadCourse.Title,
                    Description = uploadCourse.Description,
                    Duration = uploadCourse.Duration,
                    CategoryId = _unitOfWork.CategoryRepository.GetCategoryIdByName(uploadCourse.CourseCategory),
                    CoverPicture = uploadCourse.CoverPicture,
                    Price = uploadCourse.Price,
                    Rate = 0,
                    CreationDate = DateTime.Now,
                    SectionsNo = uploadCourse.SectionsNo,
                    Sections = uploadCourse == null ? null : uploadCourse.Sections.Select(section => new Section
                    {
                        Title = section.SectionTitle,
                        LessonsNo = section.NumberOfLessons,
                        Lessons = section.Lessons == null ? null : section.Lessons.Select(lesson => new Lesson
                        {
                            Title = lesson.LessonTitle,
                            Content = lesson.LessonUrl
                        }).ToList()
                    }).ToList()
                };
                _unitOfWork.CourseRepository.Create(uploadedCourse);
                _unitOfWork.SaveChanges();
                return (true, "Course Uploaded Successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
            #endregion
        }
    }
}

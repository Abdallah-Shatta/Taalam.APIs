using E_Learning.BL.DTO.Course;
using E_Learning.BL.DTO.CourseDTO.CourseSectionDTO;
using E_Learning.BL.DTO.CourseDTO.CourseSectionInfoDTO.CourseLessonDTO;
using E_Learning.BL.DTO.CourseDTO.CourseSectionInfoDTO.CourseQuizInfoDTO;
using E_Learning.BL.DTO.CourseDTO.InstructorInfoDTO;
using E_Learning.DAL.Models;
using E_Learning.DAL.UnitOfWorkDP;

namespace E_Learning.BL.Managers.CourseManager
{
    public class CourseManager: ICourseManager
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
                Instructor =  new ReadCourseInstructorInfoDTO
                {
                    Id = course.User.Id,
                    FName = course.User.FName,
                    LName = course.User.LName,
                    Description = course.User.Description

                } ,

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
                SectionsNo = course.Sections!=null? course.Sections.Count():0,

                Sections = course.Sections == null ? null : course.Sections.Select(section => new
                ReadCourseSectionInfoDTO
                {
                    Id = section.Id,
                    SectionNumber = section.SectionNumber,
                    Title = section.Title,
                    LessonsNo = section.Lessons!=null? section.Lessons.Count():0,

                    Lessons = section.Lessons==null? null: section.Lessons.Select(lesson => new ReadCourseLessonDTO

                    {
                        Id = lesson.Id,
                        Title = lesson.Title,
                        Duration = lesson.Duration,

                    }

             ).ToList(),
                    Quizes = section.Quizes==null? null: section.Quizes.Select(quiz => new ReadCourseQuizInfoDTO
                    {
                        Id = quiz.Id,
                        Title = quiz.Title,


                    }).ToList(),


                }).ToList()
            
            };

            return courseDetails;





        }


    }
}

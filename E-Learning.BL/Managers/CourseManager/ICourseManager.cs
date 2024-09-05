using E_Learning.BL.DTO.Course;
using E_Learning.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.BL.Managers.CourseManager
{
    public interface ICourseManager
    {
        ReadOneCourseDetailsDto GetCourseDetailsById(int id);

    }
}

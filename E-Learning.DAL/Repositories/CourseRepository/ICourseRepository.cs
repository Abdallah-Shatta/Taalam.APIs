using E_Learning.DAL.Models;
using E_Learning.DAL.Repositories.GenericRepository;

namespace E_Learning.DAL.Repositories.CourseRepository
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Course getCourseDetailsById(int id);

    }
}

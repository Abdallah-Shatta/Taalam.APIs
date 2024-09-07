using E_Learning.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.DAL.Repositories.RatingRepository
{
    public interface IRatingRepository
    {
        public IEnumerable<Rating> GetAllRatingsForCourse(int courseId);
        public IEnumerable<Rating> GetAllRatingGivenByUser(int userId);

    }
}

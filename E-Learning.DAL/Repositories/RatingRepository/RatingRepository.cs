using E_Learning.DAL.Data.Context;
using E_Learning.DAL.Models;
using E_Learning.DAL.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.DAL.Repositories.RatingRepository
{
    public class RatingRepository:GenericRepository<Rating>,IRatingRepository
    {
        public RatingRepository(AppDbContext _context) : base(_context)
        {

        }

        public IEnumerable<Rating> GetAllRatingGivenByUser(int userId)
        {
            var ratingList = _context.Rating
                .Include(r => r.Course)  
                .Where(r => r.UserId == userId)
                .ToList();
            return ratingList;
        }


        public IEnumerable<Rating> GetAllRatingsForCourse(int courseId)
        {
            var ratingList = _context.Rating
                .Include(r => r.User)  
                .Where(r => r.CourseId == courseId)
                .ToList();
            return ratingList;
        }
    }
}

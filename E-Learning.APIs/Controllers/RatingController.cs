using E_Learning.BL.Managers.RatingManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.APIs.Controllers
{
    [Authorize]
    public class RatingController: APIBaseController
    {
        private readonly IRatingManager _ratingManager;
        public RatingController(IRatingManager ratingManager)
        {
            this._ratingManager = ratingManager;
        }
        [HttpGet("/course/{id}")]
        [AllowAnonymous]
        public IActionResult getAllRatingsForCourse(int id)
        {
            var ratings = _ratingManager.GetAllRatingsForCourse(id);
            if (ratings == null)
            {
                return NotFound();
            }
            return Ok(ratings);
        }

        [HttpGet("/student/{id}")]
        [AllowAnonymous]
        public IActionResult GetAllRatingGivenByUser(int id)
        {
            var ratings = _ratingManager.GetAllRatingGivenByUser(id);
            if (ratings == null)
            {
                return NotFound();
            }
            return Ok(ratings);
        }

    }
}

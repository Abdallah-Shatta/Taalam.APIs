using E_Learning.BL.DTO.User;
using E_Learning.BL.Managers.CategoryManager;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : APIBaseController
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("Get-Instructor-Info")]
        public ActionResult<IEnumerable<InstructorDTO>> GetInstructorInfo(int id)
        {
            var instructorInfo = _userManager.GetInstructorInfo(id);
            if (instructorInfo == null)
                return NotFound();
            return Ok(instructorInfo);
        }
    }
}

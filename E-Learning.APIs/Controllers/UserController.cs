using E_Learning.BL.DTO.User;
using E_Learning.BL.Managers.CategoryManager;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.APIs.Controllers
{
    public class UserController : APIBaseController
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("Get-Instructor-Info/{id}")]
        public ActionResult<IEnumerable<InstructorDTO>> GetInstructorInfo(int id)
        {
            var instructorInfo = _userManager.GetInstructorInfo(id);
            if (instructorInfo == null)
                return NotFound();
            return Ok(instructorInfo);
        }

        [HttpPut("Edit-User-Profile")]
        public ActionResult EditUserProfile(EditUserProfileDTO editUserProfileDTO)
        {
            var result = _userManager.EditUserProfile(editUserProfileDTO);//EditUserProfile Calling
            if (result)
                return Ok("Your profile has updated successfully");
            return BadRequest();
        }


    }
}

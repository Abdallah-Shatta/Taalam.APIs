using E_Learning.BL.DTO.User;
using E_Learning.DAL.Models;
using E_Learning.DAL.UnitOfWorkDP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.APIs.Controllers
{
  
    public class AccountController : APIBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUnitOfWork _unitOfWork;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<Role> roleManager , IUnitOfWork unitofwork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _unitOfWork = unitofwork;
        }

        [HttpPost]
        public async Task<ActionResult<User>> AccountRegister([FromBody] RegisterDTO registerDTO)
        {

            if (ModelState.IsValid ==false)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            User user = new User()
            {
                Email=registerDTO.Email,
                PhoneNumber = registerDTO.Phone,
                UserName = registerDTO.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                //sign-in
                await _signInManager.SignInAsync(user, isPersistent: false);

                return Ok(user);
            }
            else
            {
                string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description)); //error1 | error2
                return Problem(errorMessage);
            }


        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _unitOfWork.UserRepository.GetAll();

        }



    }
}

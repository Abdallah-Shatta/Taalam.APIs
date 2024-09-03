using E_Learning.BL.DTO.User;
using E_Learning.BL.Enums;
using E_Learning.BL.Managers.AuthenticationManager;
using E_Learning.DAL.Models;
using E_Learning.DAL.UnitOfWorkDP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.APIs.Controllers
{
    [AllowAnonymous]
    public class AccountController : APIBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtManager _jwtManager;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<Role> roleManager , IUnitOfWork unitofwork,IJwtManager jwtManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _unitOfWork = unitofwork;
            _jwtManager = jwtManager;
        }

        [HttpPost("Register")]
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
                FName = registerDTO.FName
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                if (registerDTO.UserRole == UserRoleOptions.Admin)
                {

                    if (await _roleManager.FindByNameAsync(UserRoleOptions.Admin.ToString()) is null)
                    {
                        Role role = new Role() { Name = UserRoleOptions.Admin.ToString() };
                        await _roleManager.CreateAsync(role);
                    }

                   await _userManager.AddToRoleAsync(user,UserRoleOptions.Admin.ToString());
                }
                else
                {
                    if (await _roleManager.FindByNameAsync(UserRoleOptions.User.ToString()) is null)
                    {
                        Role role = new Role() { Name = UserRoleOptions.User.ToString() };
                        await _roleManager.CreateAsync(role);
                    }
                  await  _userManager.AddToRoleAsync(user,UserRoleOptions.User.ToString());
                }
                //sign-in
                await _signInManager.SignInAsync(user, isPersistent: false);
                var authenticationResponse = _jwtManager.createJwtToken(user);
                return Ok(user);
            }
            else
            {
                string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description)); //error1 | error2
                return Problem(errorMessage);
            }


        }

        [HttpGet("getusers")]
        public IEnumerable<User> GetUsers()
        {
            return _unitOfWork.UserRepository.GetAll();

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            //Validation
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }


            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                User? user = await _userManager.FindByEmailAsync(loginDTO.Email);

                if (user == null)
                {
                    return NoContent();
                }
                var authenticationResponse = _jwtManager.createJwtToken(user);

                return Ok(authenticationResponse);
            }

            else
            {
                return Problem("Invalid email or password");
            }
        }

        [HttpGet]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            User? user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }


        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }
    }

 

}


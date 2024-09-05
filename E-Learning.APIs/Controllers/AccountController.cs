using E_Learning.BL.DTO.User;
using E_Learning.BL.Enums;
using E_Learning.BL.Managers.AccountManager;
using E_Learning.BL.Managers.AuthenticationManager;
using E_Learning.BL.Managers.Mailmanager;
using E_Learning.DAL.Models;
using E_Learning.DAL.UnitOfWorkDP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Learning.APIs.Controllers
{
    [AllowAnonymous]
    public class AccountController : APIBaseController
    {
        private readonly IAccountManager _accountManager;
        private readonly IMailManager _mailManager;
        private readonly UserManager<User> _userManager;
        public AccountController(IAccountManager accountManager,IMailManager mailManager, UserManager<User> userManager)
        {
            _accountManager = accountManager;
            _mailManager = mailManager;
            _userManager = userManager;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<User>> AccountRegister([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            IdentityResult result = await _accountManager.RegisterUserAsync(registerDTO);

            if (result.Succeeded)
            {
                var user = await _accountManager.FindUserByEmailAsync(registerDTO.Email);
                return Ok(user);
            }
            else
            {
                string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
                return Problem(errorMessage);
            }
        }
            [HttpGet("getusers")]
        public IActionResult GetUsers()
        {
            var users = _accountManager.GetAllUsers();
            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            var authenticationResponse = await _accountManager.LoginAsync(loginDTO);

            if (authenticationResponse == null)
            {
                return Problem("Invalid email or password");
            }

            return Ok(authenticationResponse);
        }

        [HttpGet]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            bool isRegistered = await _accountManager.IsEmailAlreadyRegisteredAsync(email);
            return Ok(!isRegistered);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountManager.SignOutUserAsync();
            return NoContent();
        }

        [HttpPost("generate-new-jwt-token")]
        public async Task<IActionResult> GenerateNewAccessToken([FromBody] TokenModel tokenModel)
        {
            if (tokenModel == null)
            {
                return BadRequest("Invalid client request");
            }

            var authenticationResponse = await _accountManager.GenerateNewJwtTokenAsync(tokenModel);

            if (authenticationResponse == null)
            {
                return BadRequest("Invalid JWT access token or refresh token");
            }

            return Ok(authenticationResponse);
        }

        [HttpGet("forget-password")]
public async Task<IActionResult> forgetPasswordview(string email , string token)
        {
            var model = new ForgetPasswordDTO { Email = email, Token = token };
            return Ok(model);
        }

        [HttpPost("forget-password")]
        public async Task<string> forgetPassword(ForgetPasswordDTO forgetPasswordDTO)
        {
            if (await _mailManager.sendforgetemail(forgetPasswordDTO) == "sucess")
            {
                return "sucess email sending";
            }
            else
            {
                return "error";
            }

        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> resetpassword(ForgetPasswordDTO forgetPasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            var user = await _userManager.FindByEmailAsync(forgetPasswordDTO.Email);
            if (user != null)
            {
                var resetpasswordresult = await _userManager.ResetPasswordAsync(user, forgetPasswordDTO.Token, forgetPasswordDTO.Password);
                if (!resetpasswordresult.Succeeded)
                {
                    foreach(var error in resetpasswordresult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }
            }
            else{

                return Ok(new
                {
                    message = "password has been succeded reset"
                });
            }

            return BadRequest("error in reseting the password");


        }


    }

}


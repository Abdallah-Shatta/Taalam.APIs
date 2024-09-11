﻿using E_Learning.BL.DTO.Mail;
using E_Learning.BL.DTO.User;
using E_Learning.BL.Enums;
using E_Learning.BL.Managers.AccountManager;
using E_Learning.BL.Managers.AuthenticationManager;
using E_Learning.BL.Managers.Mailmanager;
using E_Learning.DAL.Models;
using E_Learning.DAL.UnitOfWorkDP;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web;

namespace E_Learning.APIs.Controllers
{
    [AllowAnonymous]
    public class AccountController : APIBaseController
    {
        private readonly IAccountManager _accountManager;
      
        public AccountController(IAccountManager accountManager,IMailManager mailManager, UserManager<User> userManager)
        {
            _accountManager = accountManager;
         
           
        }
        [HttpPost("Register")]
        public async Task<IActionResult> AccountRegister([FromBody] RegisterDTO registerDTO)
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
                LoginDTO loginDTO = new LoginDTO() { Email = user.Email , Password =registerDTO.Password};
                var authenticationResponse = await _accountManager.LoginAsync(loginDTO);
                return Ok(authenticationResponse);
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


     
        [HttpGet("signin-google")]
        public async Task LoginGoogle()
        {


            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
            {
                RedirectUri = Url.Action("ExternalLoginCallback")
            });

            //await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
            //{
            //    RedirectUri = Url.Action("ExternalLoginCallback"),
            //    Items = { { "prompt", "consent" }, { "scope", "profile email https://www.googleapis.com/auth/user.phonenumbers.read" } }
            //});

        }

        [HttpGet("external-login-callback")]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            // Retrieve authentication info from Google
            var info = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (info.Principal == null)
            {
                return Problem("Failed to authenticate via Google");
            }

            // Get user details from Google
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var name = info.Principal.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrEmpty(email))
            {
                return Problem("Google did not provide an email.");
            }

            // Check if the user already exists in the database
            var user = await _accountManager.FindUserByEmailAsync(email);

            // If the user doesn't exist, register them
            if (user == null)
            {
                GoogleregisterDTO googleregisterDTO = new GoogleregisterDTO()
                {
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                    FName = info.Principal.FindFirstValue(ClaimTypes.Name)
                };

                IdentityResult registerResult = await _accountManager.RegisterUserWithGoogle(googleregisterDTO);

                if (!registerResult.Succeeded)
                {
                    string errorMessage = string.Join(" | ", registerResult.Errors.Select(e => e.Description));
                    return Problem(errorMessage);
                }

                // Retrieve the newly registered user
                user = await _accountManager.FindUserByEmailAsync(googleregisterDTO.Email);
            }

            // Log the user in after registration or if already existed
            var authenticationResponse = await _accountManager.LoginWithGoogle(user);

            if (authenticationResponse == null)
            {
                return Problem("Login failed after registration.");
            }

            //return Ok(authenticationResponse);  // Return the JWT token or necessary response
            return Redirect($"http://localhost:4200/auth-callback?token={authenticationResponse.Token}");
        }


        [HttpGet("is-email-registered")]
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

        [HttpGet("forget-password")] //malhash lazma
public async Task<IActionResult> forgetPasswordview(string email , string token)
        {
            var model = new ForgetPasswordDTO { Email = email, Token = token };
            return Ok(model);
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDTO forgetPasswordDTO)
        {
            var (success, message) = await _accountManager.ForgetPasswordAsync(forgetPasswordDTO);
            if (success)
            {
                return Ok(new { Message = message });
            }
            else
            {
                return Problem(message);
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

            var (success, message) = await _accountManager.ResetPasswordAsync(forgetPasswordDTO);
            if (success)
            {
                return Ok(new { Message = message });
            }
            else
            {
                return BadRequest(new { Message = message });
            }



        }


    }

}


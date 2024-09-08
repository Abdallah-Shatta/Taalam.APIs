using E_Learning.BL.DTO.Mail;
using E_Learning.BL.DTO.User;
using E_Learning.BL.Enums;
using E_Learning.BL.Managers.AuthenticationManager;
using E_Learning.BL.Managers.Mailmanager;
using E_Learning.DAL.Models;
using E_Learning.DAL.UnitOfWorkDP;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace E_Learning.BL.Managers.AccountManager
{
    public class AccountManager : IAccountManager
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtManager _jwtManager;
        private readonly IMailManager _mailManager;
        public AccountManager(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<Role> roleManager, IUnitOfWork unitOfWork, IJwtManager jwtManager , IMailManager mailManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _jwtManager = jwtManager;
            _mailManager = mailManager;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterDTO registerDTO)
        {
            // Create the User entity from the DTO
            User user = new User()
            {
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.Phone,
                UserName = registerDTO.Email,
                FName = registerDTO.FName,
                LName = registerDTO.LName,
            };

            // Create the user
            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                // Determine the role and assign it
                string roleName = registerDTO.UserRole == UserRoleOptions.Admin ? UserRoleOptions.Admin.ToString() : UserRoleOptions.User.ToString();

                if (await _roleManager.FindByNameAsync(roleName) == null)
                {
                    Role role = new Role() { Name = roleName };
                    await _roleManager.CreateAsync(role);
                }

                await _userManager.AddToRoleAsync(user, roleName);

                // Sign-in the user
                await _signInManager.SignInAsync(user, isPersistent: false);

                // Generate JWT token and refresh token
                var authenticationResponse = _jwtManager.createJwtToken(user);
                user.RefreshToken = authenticationResponse.RefreshToken;
                user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDateTime;

                // Update the user with the new refresh token
                await _userManager.UpdateAsync(user);
            }

            return result;
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> LoginUserAsync(LoginDTO loginDTO)
        {
            return await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);
        }

        public async Task SignOutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<AuthenticationResponseDTO> GenerateNewJwtTokenAsync(User user, TokenModel tokenModel)
        {
            var authenticationResponse = _jwtManager.createJwtToken(user);
            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDateTime;

            await _userManager.UpdateAsync(user);

            return authenticationResponse;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public async Task<bool> IsEmailAlreadyRegisteredAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<AuthenticationResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(loginDTO.Email);

                if (user == null)
                {
                    return null;
                }

                var authenticationResponse = _jwtManager.createJwtToken(user);
                user.RefreshToken = authenticationResponse.RefreshToken;
                user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDateTime;
                await _userManager.UpdateAsync(user);

                return authenticationResponse;
            }

            return null;
        }

        public async Task<AuthenticationResponseDTO> GenerateNewJwtTokenAsync(TokenModel tokenModel)
        {
            ClaimsPrincipal? principal = _jwtManager.GetClaimsPrinciplFromJwtToken(tokenModel.Token);

            if (principal == null)
            {
                return null;
            }

            string? email = principal.FindFirstValue(ClaimTypes.Email);
            User? user = await _userManager.FindByEmailAsync(email);

            if (user == null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpirationDateTime <= DateTime.Now)
            {
                return null;
            }

            return await GenerateNewJwtTokenAsync(user, tokenModel);
        }

        public async Task<(bool Success, string Message)> ForgetPasswordAsync(ForgetPasswordDTO forgetPasswordDTO)
        {
            var user = await _userManager.FindByNameAsync(forgetPasswordDTO.Email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                string encodedToken = HttpUtility.UrlEncode(token);
                string link = $"http://localhost:4200/forget-passwordToken?token={encodedToken}&email={user.Email}";

                // HTML template for the email
                string htmlBody = $@"
                    <html>
                    <head>
                        <style>
                            .container {{ /* Your email styles here */ }}
                            .button {{ /* Your button styles here */ }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h2>Password Reset Request</h2>
                            <p class='message'>You have requested to reset your password. Click the button below to proceed.</p>
                            <a href='{link}' class='button'>Reset Password</a>
                            <p>If you did not request a password reset, please ignore this email.</p>
                            <div class='footer'>
                                <p>Thank you,<br>The E-Learning Team</p>
                            </div>
                        </div>
                    </body>
                    </html>";

                MailData mailData = new MailData
                {
                    RecieverMail = user.Email,
                    EmailSubject = "Password Reset Request",
                    EmailBody = htmlBody
                };

                // Use mail service to send email
                if (_mailManager.SendMail(mailData))
                {
                    return (true, "Email has been sent successfully.");
                }
                else
                {
                    return (false, "Error with sending email.");
                }
            }

            return (false, "User not found.");
        }
        public async Task<(bool Success, string Message)> ResetPasswordAsync(ForgetPasswordDTO forgetPasswordDTO)
        {
            if (forgetPasswordDTO == null || string.IsNullOrWhiteSpace(forgetPasswordDTO.Token))
            {
                return (false, "Invalid password reset request.");
            }

            var user = await _userManager.FindByEmailAsync(forgetPasswordDTO.Email);
            if (user != null)
            {
                var resetPasswordResult = await _userManager.ResetPasswordAsync(user, forgetPasswordDTO.Token, forgetPasswordDTO.Password);
                if (!resetPasswordResult.Succeeded)
                {
                    var errors = string.Join(" | ", resetPasswordResult.Errors.Select(e => e.Description));
                    return (false, errors);
                }

                return (true, "Password has been reset successfully.");
            }

            return (false, "User does not exist.");
        }
    }
}

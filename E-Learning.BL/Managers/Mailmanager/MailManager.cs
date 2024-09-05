using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using E_Learning.BL.DTO.User;
using E_Learning.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using SendGrid;
using SendGrid.Helpers.Mail;
namespace E_Learning.BL.Managers.Mailmanager
{
    public class MailManager : IMailManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public MailManager(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

       

        public async Task<string> sendforgetemail(ForgetPasswordDTO forgetPasswordDTO)
        {
            var user = await _userManager.FindByNameAsync(forgetPasswordDTO.Email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var urlbuilder = new UriBuilder();
                string link = $"http://localhost:5062/api/Account/ForgetPassword?token={token}&email={user.Email}";

                //send the email with this url

                return "the forget password email is sent";

            }
            else
            {
                return "error";
            }
        }

        public string sendrandommail(string subject, string body)
        {
            return "hello";
        }
     


    }
}




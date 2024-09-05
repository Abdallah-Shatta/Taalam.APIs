using E_Learning.BL.DTO.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.BL.Managers.Mailmanager
{
    public interface IMailManager
    {
        string sendrandommail(string subject, string body);

        Task<string> sendforgetemail(ForgetPasswordDTO forgetPasswordDTO);

    }
}

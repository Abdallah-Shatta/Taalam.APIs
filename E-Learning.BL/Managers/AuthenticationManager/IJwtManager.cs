﻿using E_Learning.BL.DTO.User;
using E_Learning.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.BL.Managers.AuthenticationManager
{
    public interface IJwtManager
    {
        AuthenticationResponseDTO createJwtToken(User user);
    }
}

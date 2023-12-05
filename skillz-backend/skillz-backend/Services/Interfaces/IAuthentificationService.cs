using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using skillz_backend.models;

namespace skillz_backend.Services
{

    public interface IAuthenticationService
    {
        string GenerateToken(User user);

    }

}
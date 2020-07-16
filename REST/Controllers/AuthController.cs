using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PrjWebApi01.Models;
using PrjWebApi01.Models.Repository;
using PrjWebApi01.Models.DTO;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace PrjWebApi01.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IConfiguration Config;

        public AuthController(IConfiguration configuration)
        {
            Config = configuration; 
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody]AuthDTO auth)
        {

            JwtSecurityToken token;
            DateTime expiration;
            
            var llave = Encoding.UTF8.GetBytes(Config["Tokens:Key"]);
            var key = new SymmetricSecurityKey(llave);
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]{
              new Claim(ClaimTypes.Sid, auth.Guid),
              new Claim(ClaimTypes.Role, auth.Role),
              new Claim("Organization",auth.Organization)
            };
            token = new JwtSecurityToken(Config["Tokens:Issuer"],
                                         Config["Tokens:Issuer"],
                                         claims,
                                         expires: DateTime.Now.AddDays(1),
                                         signingCredentials: creds);

            string tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);
            expiration = token.ValidTo;
            return Ok(tokenHandler);
        }
    }
}
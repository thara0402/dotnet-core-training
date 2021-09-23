using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtConfig _jwtConfig;

        public TokenController(JwtConfig jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody] Login login)
        {
            var user = Authenticate(login);
            if (user != null)
            {
                return Ok(new { token = BuildToken(user) });
            }
            return Unauthorized();
        }

        private User Authenticate(Login login)
        {
            if (login.Username == "admin" && login.Password == "P@ssw0rd")
            {
                return new User { Name = "Administrator", Email = "admin@sample.com" };
            }
            return null;
        }

        private string BuildToken(User user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
              };
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                                _jwtConfig.Issuer,
                                _jwtConfig.Issuer,
                                claims,
                                expires: DateTime.Now.AddMinutes(30),
                                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

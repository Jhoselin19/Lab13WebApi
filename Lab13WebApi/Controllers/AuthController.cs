using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Lab13WebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace Lab13WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User { UserName = "admin", Password = "password" }
        };

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var existingUser = users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
            if (existingUser != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("your_super_secret_key_that_is_at_least_32_chars_long"); // Debe ser una clave secreta segura y de al menos 32 caracteres
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized(new { message = "Username or password is incorrect" });
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User newUser)
        {
            if (users.Any(u => u.UserName == newUser.UserName))
            {
                return BadRequest(new { message = "Username already exists" });
            }

            users.Add(newUser);
            return Ok(new { message = "User registered successfully" });
        }
    }
}

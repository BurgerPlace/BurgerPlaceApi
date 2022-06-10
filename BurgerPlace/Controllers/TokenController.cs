using BurgerPlace.Context;
using BurgerPlace.Models;
using BurgerPlace.Models.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BurgerPlace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        IConfiguration _config;
        private string secret { get; set; }
        public TokenController(IConfiguration config)
        {
            _config = config;
            secret = _config.GetValue<string>("Secrets:JWTSecret");
        }
        private static bool IsValidUser(LoginRequest request)
        {
            // This is where you would look the user up in the database.
            using (var context = new BurgerPlaceContext())
            {
                var t = context.Users.Where(i => i.Username == request.username && i.Password == request.password).FirstOrDefault();
                if (t != null) return true;
                return false;
            }
        }

        private async Task<User> findUser(LoginRequest request)
        {
            using (var context = new BurgerPlaceContext())
            {
                var t = await context.Users.Where(i => i.Username == request.username && i.Password == request.password).FirstAsync();
                return t;
            }
        }

        private string GenerateToken(User user)
        {
            string role = "user";
            if (user.IsRoot) role = "root";
            var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Exp,
            new DateTimeOffset(DateTime.Now)
                   .ToUnixTimeSeconds().ToString())
            };

            var header = new JwtHeader(
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secret)),
                        SecurityAlgorithms.HmacSha256));

            var payload = new JwtPayload(claims);

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        public IActionResult GetToken(LoginRequest loginRequest)
        {
            if (IsValidUser(loginRequest))
            {
                User user = findUser(loginRequest).Result;
                string token = GenerateToken(user);
                return Ok(token);
            }
            else
            {
                return BadRequest("Invalid username and/or password.");
            }
        }
        public record LoginRequest(string username, string password);

        [HttpGet]
        [Authorize]
        public IActionResult VerifyToken()
        {
            return Ok("Congratulations, you are authorized.");
        }

    }
}

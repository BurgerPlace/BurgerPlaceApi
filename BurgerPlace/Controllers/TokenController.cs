using BurgerPlace.Context;
using BurgerPlace.Models;
using BurgerPlace.Models.Database;
using BurgerPlace.Models.Request.Users;
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
        private string _secret { get; set; }
        public TokenController(IConfiguration config)
        {
            _config = config;
            _secret = _config.GetValue<string>("Secrets:JWTSecret");
        }

        /// <summary>
        /// Checking if <see cref="User">User</see> has valid credentials
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// Boolean value, representing if user is valid
        /// </returns>
        private static bool IsValidUser(LoginRequest request)
        {
            using (var context = new BurgerPlaceContext())
            {
                var t = context.Users.Where(i => i.Username == request.username && i.Password == request.password).FirstOrDefault();
                if (t != null) return true;
                return false;
            }
        }

        /// <summary>
        /// Finds <see cref="User">User</see> with provided credentials
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<User> findUser(LoginRequest request)
        {
            using (var context = new BurgerPlaceContext())
            {
                var user = await context.Users.Where(i => i.Username == request.username && i.Password == request.password).FirstAsync();
                return user;
            }
        }

        /// <summary>
        /// Generates <see href="https://jwt.io">JWT Token</see> for provided <see cref="User">User</see>
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// <see href="https://jwt.io">JWT Token</see>
        /// </returns>
        private string GenerateToken(User user)
        {
            string role = "user";
            if (user.IsRoot) role = "root";
            var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Exp,
            new DateTimeOffset(DateTime.Now)
                   .ToUnixTimeSeconds().ToString())
            };

            var header = new JwtHeader(
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_secret)),
                        SecurityAlgorithms.HmacSha256));

            var payload = new JwtPayload(claims);

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Generates <see href="https://jwt.io">JWT Token</see> for provided <see cref="User">User</see>
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// <see href="https://jwt.io">JWT Token</see>
        /// </returns>
        public static string GenerateToken(User user, string _secret)
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
                        Encoding.UTF8.GetBytes(_secret)),
                        SecurityAlgorithms.HmacSha256));

            var payload = new JwtPayload(claims);

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Allows for manually generating <see href="https://jwt.io">JWT Tokens</see> for <see cref="User">User</see>
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>
        /// <see href="https://jwt.io">JWT Token</see>
        /// </returns>
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
        /// <summary>
        /// Allows for checking if <see href="https://jwt.io">JWT Token</see> and if <see cref="User">User</see> is authorized
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult VerifyToken()
        {
            return Ok("Congratulations, you are authorized.");
        }

    }
}

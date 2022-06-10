using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BurgerPlace.Models.Database;
using BurgerPlace.Models.Request;
using System.Security.Cryptography;
using BurgerPlace.Context;
using AutoMapper;

namespace BurgerPlace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _configuration;
        private string _secret;
        private IMapper _mapper;
        public UserController(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
            _secret = configuration.GetValue<string>("Secrets:PasswordSalt");
        }

        // Registering new user
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterRequest registerRequest)
        {
            using (var context = new BurgerPlaceContext())
            {
                // Assertion that this user already doesn't exists in database
                var user = context.Users.Where(i => i.Username == registerRequest.username);
                if (user != null)
                {
                    return BadRequest(new
                    {
                        message = "User with this username already exists"
                    });
                }
                // Creating new user based on given informations
                User userToCreate = _mapper.Map<RegisterRequest, User>(registerRequest);
                userToCreate.LastLogin = DateTime.Now;
                // Hashing the password
                var passwordHashed = BCrypt.Net.BCrypt.HashPassword(registerRequest.password, _secret);
                userToCreate.Password = passwordHashed;
                await context.AddAsync(userToCreate);
                await context.SaveChangesAsync();
                return Ok(new
                {
                    message = "Successfully created new user account"
                });
            }
        }

        // Login process
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginRequest registerRequest)
        {
            using (var context = new BurgerPlaceContext())
            {

            }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BurgerPlace.Models.Database;
using BurgerPlace.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static BurgerPlace.Models.Response.Users.LoginResponse;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using static BurgerPlace.Models.Response.Users.RegisterResponse;
using BurgerPlace.Models.Request.Users;
using BurgerPlace.Models.Response.Users;

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
            _secret = configuration.GetValue<string>("Secrets:JWTSecret");
        }

        /// <summary>
        /// Registers a new User and adds it to database
        /// </summary>
        /// <param name="registerRequest">Model of User which we want to register</param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(SuccessfullyCreatedNewUserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DuplicatedUsernameOrEmailResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest registerRequest)
        {
            using (var context = new BurgerPlaceContext())
            {
                // Assertion that this user already doesn't exists in database
                var user = await context.Users.Where(i => i.Username == registerRequest.username || i.Email == registerRequest.email).FirstOrDefaultAsync();
                if (user != null)
                {
                    return BadRequest(new DuplicatedUsernameOrEmailResponse());
                }
                // Creating new user based on given informations
                User userToCreate = _mapper.Map<RegisterRequest, User>(registerRequest);
                userToCreate.LastLogin = DateTime.Now;
                // Hashing the password
                var passwordHashed = BCrypt.Net.BCrypt.HashPassword(registerRequest.password);
                userToCreate.Password = passwordHashed;
                await context.AddAsync(userToCreate);
                await context.SaveChangesAsync();
                return Ok(new SuccessfullyCreatedNewUserResponse());
            }
        }

        /// <summary>
        /// Provides User a JWT token if correct credentials 
        /// </summary>
        /// <param name="loginRequest">Login model</param>
        /// <returns>
        /// JWT token which will allow for further authentication
        /// </returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginSuccessResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(LoginWrongData), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest loginRequest)
        {
            using (var context = new BurgerPlaceContext())
            {
                // Veryfing if user exists
                var t = await context.Users.Where(i => i.Username == loginRequest.username).FirstOrDefaultAsync();
                if (t != null)
                {
                    // Verify password
                    if (BCrypt.Net.BCrypt.Verify(loginRequest.password, t.Password))
                    {
                        // Generate token
                        var token = TokenController.GenerateToken(t, _secret);
                        // Add timestamp to db
                        t.LastLogin = DateTime.UtcNow;
                        await context.SaveChangesAsync();
                        // after generation return
                        return Ok(new LoginSuccessResponse(token));
                    }
                    else
                    {
                        return BadRequest(new LoginWrongData());
                    }
                }
                else
                {
                    return BadRequest(new LoginWrongData());
                }
            }
        }

        /// <summary>
        /// Rises privileges of User specified in request to root
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Root)]
        [HttpPut("makeRoot")]
        [ProducesResponseType(typeof(MakeUserRootResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InvalidUserResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]

        public async Task<IActionResult> MakeUserRoot([FromBody] MakeUserRootRequest request)
        {
            using (var context = new BurgerPlaceContext())
            {
                // Finding proper user
                var user = await context.Users.Where(i => i.Username == request.username).FirstOrDefaultAsync();
                if (user == null)
                {
                    return BadRequest(new InvalidUserResponse());
                }
                user.IsRoot = true;
                await context.SaveChangesAsync();
                return Ok(new MakeUserRootResponse(user.Username));
            }
        }

        /// <summary>
        /// Lowers privileges of User specified in request to user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Root)]
        [HttpPut("removeRoot")]
        [ProducesResponseType(typeof(MakeUserRootResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InvalidUserResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> MakeUserUser([FromBody] MakeUserRootRequest request)
        {
            using (var context = new BurgerPlaceContext())
            {
                // Finding proper user
                var user = await context.Users.Where(i => i.Username == request.username).FirstOrDefaultAsync();
                if (user == null)
                {
                    return BadRequest(new InvalidUserResponse());
                }
                user.IsRoot = false;
                await context.SaveChangesAsync();
                return Ok(new MakeUserRootResponse(user.Username));
            }
        }

        /// <summary>
        /// Removes User from database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Root)]
        [HttpDelete("user")]
        [ProducesResponseType(typeof(DeleteUserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InvalidUserResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUser request)
        {
            using (var context = new BurgerPlaceContext())
            {
                // Finding proper user
                var user = await context.Users.Where(i => i.Username == request.username).FirstOrDefaultAsync();
                if (user == null)
                {
                    return BadRequest(new InvalidUserResponse());
                }
                context.Remove(user);
                await context.SaveChangesAsync();
                return Ok(new DeleteUserResponse(user.Username));
            }
        }
    }
}

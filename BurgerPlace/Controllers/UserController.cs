﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BurgerPlace.Models.Database;
using BurgerPlace.Models.Request;
using System.Security.Cryptography;
using BurgerPlace.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static BurgerPlace.Models.Response.LoginResponse;

namespace BurgerPlace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _configuration;
        private string _salt;
        private string _secret;
        private IMapper _mapper;
        public UserController(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
            _salt = configuration.GetValue<string>("Secrets:PasswordSalt");
            _secret = configuration.GetValue<string>("Secrets:JWTSecret");
        }

        // Registering new user
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest registerRequest)
        {
            using (var context = new BurgerPlaceContext())
            {
                // Assertion that this user already doesn't exists in database
                var user = await context.Users.Where(i => i.Username == registerRequest.username).FirstOrDefaultAsync();
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
                var passwordHashed = BCrypt.Net.BCrypt.HashPassword(registerRequest.password);
                userToCreate.Password = passwordHashed;
                await context.AddAsync(userToCreate);
                await context.SaveChangesAsync();
                return Ok(new
                {
                    message = "Successfully created new user account"
                });
            }
        }

        // Login user
        [HttpPost("login")]
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
                        t.LastLogin = DateTime.Now;
                        await context.SaveChangesAsync();
                        // after generation return
                        return Ok(new LoginSuccessResponse(token));
                    }
                    else
                    {
                        return BadRequest(new LoginWrongPassword());
                    }
                }
                else
                {
                    return BadRequest(new LoginWrongUsername());
                }
            }
        }
    }
}
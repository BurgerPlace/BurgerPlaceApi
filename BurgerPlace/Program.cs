using BurgerPlace.Mapper_Profiles;
using BurgerPlace.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text;

namespace BurgerPlace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters(){
                    ValidateIssuerSigningKey = true,
                    // TODO: Setup key here
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Use a password that has at least 16 characters")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(50)
                };
            });
            // Adding Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                "v1", new OpenApiInfo
                {
                    Title = "Burger Place API",
                    Version = "v1"
                });

                // This section allows submitting the token with your request in Swagger.
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                // This section allows submitting the token with your request in Swagger.
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
                }
                });
            });
            // Adding AutoMapper
            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(configurePolicy =>
            {
                // CORS only for testing purposes
                configurePolicy.AllowAnyHeader();
                configurePolicy.AllowAnyMethod();
                configurePolicy.AllowAnyOrigin();
            });

            // Setup custom unauthorized message

            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    await context.Response.WriteAsJsonAsync(new
                    {
                        message = "Unauthorized"
                    });
                }
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
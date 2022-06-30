using AutoMapper;
using BurgerPlace.Models.Database;
using BurgerPlace.Models.Request.Users;
using BurgerPlace.Models.Response.Categories;

namespace BurgerPlace.Mapper_Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterRequest, User>();
            CreateMap<Category, CategoryMapped>();
        }
    }
}

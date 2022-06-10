using AutoMapper;
using BurgerPlace.Models.Database;
using BurgerPlace.Models.Request;

namespace BurgerPlace.Mapper_Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterRequest, User>();
        }
    }
}

using AutoMapper;
using CoastlineServer.DAL.Entities;
using CoastlineServer.Service.Models;

namespace CoastlineServer.Service.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserForCreationDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
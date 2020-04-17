using AutoMapper;
using CoastlineServer.DAL.Entities;
using CoastlineServer.Service.Models;

namespace CoastlineServer.Service.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>()
                .ForMember(
                    dest => dest.Id,
                    act => act.Ignore());
            CreateMap<User, UserDTO>();
        }
    }
}

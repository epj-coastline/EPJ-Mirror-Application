using AutoMapper;
using CoastlineServer.DAL.Entities;
using CoastlineServer.Service.Models;

namespace CoastlineServer.Service.Profiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<Member, MemberDto>();
        }
    }
}
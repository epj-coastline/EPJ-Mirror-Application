using AutoMapper;
using CoastlineServer.DAL.Entities;
using CoastlineServer.Service.Models;

namespace CoastlineServer.Service.Profiles
{
    public class StrengthProfile : Profile
    {
        public StrengthProfile()
        {
            CreateMap<Strength, StrengthDto>();
        }
    }
}
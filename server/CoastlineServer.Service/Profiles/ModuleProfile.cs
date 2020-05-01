using AutoMapper;
using CoastlineServer.DAL.Entities;
using CoastlineServer.Service.Models;

namespace CoastlineServer.Service.Profiles
{
    public class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            CreateMap<Module, ModuleDto>();
        }
    }
}
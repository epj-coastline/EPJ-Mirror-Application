using AutoMapper;
using CoastlineServer.DAL.Entities;
using CoastlineServer.Service.Models;

namespace CoastlineServer.Service.Profiles
{
    public class StudyGroupProfile : Profile
    {
        public StudyGroupProfile()
        {
            CreateMap<StudyGroup, StudyGroupDto>();
            CreateMap<StudyGroupForCreationDto, StudyGroup>();
        }
    }
}
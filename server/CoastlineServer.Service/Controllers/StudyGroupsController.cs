using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CoastlineServer.DAL.Entities;
using CoastlineServer.Repository;
using CoastlineServer.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoastlineServer.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudyGroupsController : ControllerBase
    {
        private readonly StudyGroupRepository _studyGroupRepository;
        private readonly IMapper _mapper;

        public StudyGroupsController(StudyGroupRepository studyGroupRepository, IMapper mapper)
        {
            _studyGroupRepository = studyGroupRepository
                                    ?? throw new ArgumentNullException(nameof(studyGroupRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudyGroupDto>>> GetStudyGroups()
        {
            var studyGroups = await _studyGroupRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<StudyGroupDto>>(studyGroups));
        }
    }
}
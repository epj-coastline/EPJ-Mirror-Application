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

        [HttpGet("{studyGroupId:int}", Name = "GetStudyGroup")]
        public async Task<ActionResult<StudyGroupDto>> GetStudyGroup(int studyGroupId)
        {
            try
            {
                var user = await _studyGroupRepository.Get(studyGroupId);
                return Ok(_mapper.Map<StudyGroupDto>(user));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<StudyGroupDto>> CreateStudyGroup(
            StudyGroupForCreationDto studyGroupForCreationDto)
        {
            var studyGroup = _mapper.Map<StudyGroup>(studyGroupForCreationDto);
            studyGroup.CreationDate = DateTime.Now;
            var studyGroupEntity = await _studyGroupRepository.Insert(studyGroup);
            var studyGroupDto = _mapper.Map<StudyGroupDto>(studyGroupEntity);
            return CreatedAtRoute("GetStudyGroup", new
            {
                studyGroupId = studyGroupDto.Id
            }, studyGroupDto);
        }
        
        public async Task<IActionResult> DeleteUser(int studyGroupId)
        {
            try
            {
                var studyGroup = await _studyGroupRepository.Get(studyGroupId);
                await _studyGroupRepository.Delete(studyGroup);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
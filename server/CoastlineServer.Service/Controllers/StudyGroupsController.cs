using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CoastlineServer.DAL.Entities;
using CoastlineServer.Repository;
using CoastlineServer.Repository.Parameters;
using CoastlineServer.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoastlineServer.Service.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<StudyGroupDto>>> GetStudyGroups(
            [FromQuery] StudyGroupResourceParameters studyGroupResourceParameters)
        {
            try
            {
                var studyGroups = await _studyGroupRepository.GetAll(studyGroupResourceParameters);

                if (studyGroups == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<IEnumerable<StudyGroupDto>>(studyGroups));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{studyGroupId:int}", Name = "GetStudyGroup")]
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult<StudyGroupDto>> CreateStudyGroup(
            StudyGroupForCreationDto studyGroupForCreationDto)
        {
            var studyGroup = _mapper.Map<StudyGroup>(studyGroupForCreationDto);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value.Substring(6);
            studyGroup.UserId = userId;
            studyGroup.CreationDate = DateTime.UtcNow;
            var studyGroupEntity = await _studyGroupRepository.Insert(studyGroup);
            var studyGroupDto = _mapper.Map<StudyGroupDto>(studyGroupEntity);

            return CreatedAtRoute("GetStudyGroup", new
            {
                studyGroupId = studyGroupDto.Id
            }, studyGroupDto);
        }

        [HttpDelete("{studyGroupId:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int studyGroupId)
        {
            try
            {
                var studyGroup = await _studyGroupRepository.Get(studyGroupId);
                await _studyGroupRepository.Delete(studyGroup);

                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpOptions]
        [Authorize]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,OPTIONS,DELETE");

            return Ok();
        }
    }
}
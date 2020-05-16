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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoastlineServer.Service.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(
            [FromQuery] UserResourceParameters userResourceParameters)
        {
            try
            {
                var users = await _userRepository.GetAll(userResourceParameters);

                if (users == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{userId}", Name = "GetUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUser(string userId)
        {
            try
            {
                var user = await _userRepository.Get(userId);

                return Ok(_mapper.Map<UserDto>(user));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UserDto>> CreateUser(UserForCreationDto userForCreationDto)
        {
            var user = _mapper.Map<User>(userForCreationDto);
            user.Id = this.User.FindFirst(ClaimTypes.NameIdentifier).Value.Substring(6);
            var userEntity = await _userRepository.Insert(user);
            var userDto = _mapper.Map<UserDto>(userEntity);

            return CreatedAtRoute("GetUser", new
            {
                userId = userDto.Id
            }, userDto);
        }

        [HttpPut("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string id, UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            if (id != this.User.FindFirst(ClaimTypes.NameIdentifier).Value.Substring(6))
            {
                return Forbid();
            }

            var user = _mapper.Map<User>(userDto);
            try
            {
                await _userRepository.Update(user);
            }
            catch (Exception)
            {
                return Conflict();
            }

            return NoContent();
        }

        [HttpDelete("{userId}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                var user = await _userRepository.Get(userId);
                await _userRepository.Delete(user);

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
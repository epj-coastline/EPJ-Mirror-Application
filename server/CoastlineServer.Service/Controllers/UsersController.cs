using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CoastlineServer.DAL.Entities;
using CoastlineServer.Repository;
using CoastlineServer.Service.Models;

namespace CoastlineServer.Service.Controllers
{
    [ApiController]
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
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userRepository.GetAll();

            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        [HttpGet("{userId:int}", Name = "GetUser")]
        public async Task<ActionResult<UserDto>> GetUser(int userId)
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
        public async Task<ActionResult<UserDto>> CreateUser(UserForCreationDto userForCreationDto)
        {
            var user = _mapper.Map<User>(userForCreationDto);
            var userEntity = await _userRepository.Insert(user);
            var userDto = _mapper.Map<UserDto>(userEntity);

            return CreatedAtRoute("GetUser", new
            {
                userId = userDto.Id
            }, userDto);
        }

        [HttpDelete("{userId:int}")]
        public async Task<IActionResult> DeleteUser(int userId)
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
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,OPTIONS,DELETE");

            return Ok();
        }
    }
}
using System;
using System.Collections.Generic;
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
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            var users = _userRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public ActionResult<UserDto> GetUser(int userId)
        {
            var user = _userRepository.Get(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPost]
        public ActionResult<UserDto> CreateUser(UserDto userDto)
        {
            var userEntity = _mapper.Map<User>(userDto);
            _userRepository.Insert(userEntity);
            return CreatedAtRoute("GetUser", new
            {
                userId = userDto.Id
            }, userDto);
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var user = _userRepository.Get(userId);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.Delete(user);
            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,OPTIONS,DELETE");
            return Ok();
        }
    }
}
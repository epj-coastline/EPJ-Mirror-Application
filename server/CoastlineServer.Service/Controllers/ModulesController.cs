using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CoastlineServer.Repository;
using CoastlineServer.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoastlineServer.Service.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("[controller]")]
    public class ModulesController : ControllerBase
    {
        private readonly ModuleRepository _moduleRepository;
        private readonly IMapper _mapper;

        public ModulesController(ModuleRepository moduleRepository, IMapper mapper)
        {
            _moduleRepository = moduleRepository ?? throw new ArgumentNullException(nameof(moduleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModules()
        {
            var modules = await _moduleRepository.GetAll();

            return Ok(_mapper.Map<IEnumerable<ModuleDto>>(modules));
        }
    }
}
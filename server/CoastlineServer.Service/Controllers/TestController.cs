using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CoastlineServer.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private IConfiguration _configuration;

        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public String GetTest()
        {
            return _configuration["ConnectionStringCoastline"] + " DatabaseMigrations: " + _configuration["DatabaseMigrations"] + "; AllowedHosts: " + _configuration["AllowedHosts"] + ";"; 
        }
    }
}
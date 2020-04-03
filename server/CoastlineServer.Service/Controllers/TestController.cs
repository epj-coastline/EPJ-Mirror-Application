using System;
using CoastlineServer.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CoastlineServer.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private IConfiguration _configuration;
        private CoastlineContext _context;

        public TestController(IConfiguration configuration, CoastlineContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        public String GetTest()
        {
            _context.Database.Migrate();
            return _configuration["ConnectionStringCoastline"] + " DatabaseMigrations: " +
                   _configuration["DatabaseMigrations"] + "; AllowedHosts: " + _configuration["AllowedHosts"] + ";";
        }
    }
}
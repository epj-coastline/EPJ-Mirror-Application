using System.Collections.Generic;

namespace CoastlineServer.Service.Models
{
    public class ModuleDto
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public string Responsibility { get; set; }
    }
}
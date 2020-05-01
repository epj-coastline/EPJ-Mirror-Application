using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoastlineServer.Service.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Biography { get; set; }
        public string DegreeProgram { get; set; }
        public string StartDate { get; set; }
        public ICollection<StrengthDto> Strengths { get; set; } = new List<StrengthDto>();
    }
}
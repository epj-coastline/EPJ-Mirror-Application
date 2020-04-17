using System.ComponentModel.DataAnnotations;

namespace CoastlineServer.Service.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required, StringLength(20)]
        public string FirstName { get; set; }
        [Required, StringLength(20)]
        public string LastName { get; set; }
        [Required, EmailAddress(ErrorMessage = "Enter valid Email address")]
        public string Email { get; set; }
        [StringLength(140)]
        public string Biography { get; set; }
        [Required]
        public string DegreeProgram { get; set; }
        [Required, StringLength(6)]
        public string StartDate { get; set; }
    }
}
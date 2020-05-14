using System.ComponentModel.DataAnnotations;

namespace CoastlineServer.Service.Models
{
    public class StudyGroupForCreationDto
    {
        [Required, StringLength(140)] public string Purpose { get; set; }
        [Required] public int ModuleId { get; set; }
    }
}
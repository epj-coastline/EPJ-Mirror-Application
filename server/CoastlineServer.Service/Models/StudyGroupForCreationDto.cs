using System;
using System.ComponentModel.DataAnnotations;
using CoastlineServer.DAL.Entities;
using Newtonsoft.Json;

namespace CoastlineServer.Service.Models
{
    public class StudyGroupForCreationDto
    {
        [Required, StringLength(40)]
        public string Purpose { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ModuleId { get; set; }
    }
}
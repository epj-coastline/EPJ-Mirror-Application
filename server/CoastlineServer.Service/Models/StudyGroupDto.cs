using System;
using System.Collections;
using System.Collections.Generic;
using CoastlineServer.DAL.Entities;

namespace CoastlineServer.Service.Models
{
    public class StudyGroupDto
    {
        public int Id { get; set; }
        public string Purpose { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserId { get; set; }
        public UserDto User { get; set; }
        public int ModuleId { get; set; }
        public ModuleDto Module { get; set; }
        public ICollection<MemberDto> Members { get; set; } = new List<MemberDto>();
    }
}
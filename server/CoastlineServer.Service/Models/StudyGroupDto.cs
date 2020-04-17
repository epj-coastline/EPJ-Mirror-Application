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
        public byte[] RowVersion { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        // TODO
        //public ICollection<Member> Members { get; set; } = new List<Member>();
    }
}
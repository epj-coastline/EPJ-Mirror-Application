using System;
using System.Collections;
using System.Collections.Generic;

namespace CoastlineServer.DAL.Entities
{
    public class StudyGroup
    {
        public int Id { get; set; }
        public string Purpose { get; set; }
        public DateTime CreationDate { get; set; }
        public byte[] RowVersion { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public ICollection<Member> Members { get; set; } = new List<Member>();
    }
}
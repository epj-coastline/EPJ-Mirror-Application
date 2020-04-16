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
        public int CreatorId { get; set; }
        public User Creator { get; set; }
        public ICollection<Member> Type { get; set; }
    }
}
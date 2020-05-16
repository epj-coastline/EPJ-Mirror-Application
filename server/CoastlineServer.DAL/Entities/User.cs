using System.Collections;
using System.Collections.Generic;

namespace CoastlineServer.DAL.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Biography { get; set; }
        public string DegreeProgram { get; set; }
        public string StartDate { get; set; }
        public byte[] RowVersion { get; set; }
        public ICollection<Member> Members { get; set; } = new List<Member>();
        public ICollection<StudyGroup> StudyGroups { get; set; } = new List<StudyGroup>();
        public ICollection<Strength> Strengths { get; set; } = new List<Strength>();
        public ICollection<Confirmation> Confirmations { get; set; } = new List<Confirmation>();
    }
}
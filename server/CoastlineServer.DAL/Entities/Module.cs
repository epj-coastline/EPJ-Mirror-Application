using System.Collections.Generic;

namespace CoastlineServer.DAL.Entities
{
    public class Module
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public string Responsibility { get; set; }
        public byte[] RowVersion { get; set; }
        public ICollection<StudyGroup> StudyGroups { get; set; } = new List<StudyGroup>();
        public ICollection<Strength> Strengths { get; set; } = new List<Strength>();
    }
}
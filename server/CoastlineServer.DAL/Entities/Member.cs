using System;

namespace CoastlineServer.DAL.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public DateTime AccessionDate { get; set; }
        public byte[] RowVersion { get; set; }
        public int StudyGroupId { get; set; }
        public StudyGroup StudyGroup { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
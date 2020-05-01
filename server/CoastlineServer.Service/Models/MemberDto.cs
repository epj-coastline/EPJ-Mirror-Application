using System;

namespace CoastlineServer.Service.Models
{
    public class MemberDto
    {
        public int Id { get; set; }
        public DateTime AccessionDate { get; set; }
        public int StudyGroupId { get; set; }
        public int UserId { get; set; }
    }
}
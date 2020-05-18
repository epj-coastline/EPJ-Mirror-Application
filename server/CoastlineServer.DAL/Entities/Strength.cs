using System.Collections.Generic;

namespace CoastlineServer.DAL.Entities
{
    public class Strength
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public ICollection<Confirmation> Confirmations { get; set; } = new List<Confirmation>();
    }
}
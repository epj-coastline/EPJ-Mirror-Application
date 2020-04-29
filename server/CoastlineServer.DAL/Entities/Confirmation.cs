namespace CoastlineServer.DAL.Entities
{
    public class Confirmation
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int StrengthId { get; set; }
        public Strength Strength { get; set; }
    }
}
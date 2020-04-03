namespace CoastlineServer.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Biography { get; set; }
        public string DegreeProgram { get; set; }
        public string StartDate { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
using CoastlineServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoastlineServer.DAL.Context
{
    public class CoastlineContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<StudyGroup> StudyGroups { get; set; }
        public DbSet<Member> Members { get; set; }

        public CoastlineContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserTypeConfig());
            modelBuilder.ApplyConfiguration(new StudyGroupTypeConfig());
            modelBuilder.ApplyConfiguration(new MemberTypeConfig());
        }
    }
}
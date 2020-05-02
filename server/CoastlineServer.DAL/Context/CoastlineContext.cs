using CoastlineServer.DAL.Configurations;
using CoastlineServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoastlineServer.DAL.Context
{
    public class CoastlineContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<StudyGroup> StudyGroups { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Strength> Strengths { get; set; }
        public DbSet<Confirmation> Confirmations { get; set; }

        public CoastlineContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserTypeConfig());
            modelBuilder.ApplyConfiguration(new StudyGroupTypeConfig());
            modelBuilder.ApplyConfiguration(new MemberTypeConfig());
            modelBuilder.ApplyConfiguration(new ModuleTypeConfig());
            modelBuilder.ApplyConfiguration(new StrengthTypeConfig());
            modelBuilder.ApplyConfiguration(new ConfirmationTypeConfig());
        }
    }
}
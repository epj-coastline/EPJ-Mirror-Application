using CoastlineServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoastlineServer.DAL.Context
{
    public class CoastlineContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public CoastlineContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserTypeConfig());
        }
    }
}
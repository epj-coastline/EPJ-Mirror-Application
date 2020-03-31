using CoastlineServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoastlineServer.DAL.Context
{
    public class CoastlineContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public CoastlineContext(DbContextOptions<CoastlineContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var userBuilder = modelBuilder.Entity<User>();
            userBuilder.ToTable("Users").HasKey(e => e.Id);
            userBuilder.Property(e => e.Id).ValueGeneratedOnAdd();
            userBuilder.Property(e => e.RowVersion).IsRowVersion();
            userBuilder.Property(e => e.Biography).HasColumnType("VARCHAR(140)");
            userBuilder.Property(e => e.StartDate).HasColumnType("VARCHAR(6)");
            userBuilder.Property(e => e.FirstName).HasColumnType("VARCHAR(20)");
            userBuilder.Property(e => e.LastName).HasColumnType("VARCHAR(20)");

            userBuilder.HasData(
                new User()
                {
                    Id = -1,
                    FirstName = "David",
                    LastName = "Luthiger",
                    DegreeProgram = "Informatik",
                    Biography = "Start HS2018",
                    Email = "david.luthiger@hsr.ch",
                    StartDate = "HS2018"
                },
                new User()
                {
                    Id = -2,
                    FirstName = "Fabian",
                    LastName = "Germann",
                    DegreeProgram = "Informatik",
                    Biography = "Start HS2018",
                    Email = "fabian.germann@hsr.ch",
                    StartDate = "HS2018"
                },
                new User()
                {
                    Id = -3,
                    FirstName = "Eliane",
                    LastName = "Schmidli",
                    DegreeProgram = "Informatik",
                    Biography = "Start HS2018",
                    Email = "eliane.schmidli@hsr.ch",
                    StartDate = "HS2018"
                },
                new User()
                {
                    Id = -4,
                    FirstName = "Yves",
                    LastName = "Boillat",
                    DegreeProgram = "Informatik",
                    Biography = "Start HS2018",
                    Email = "yves.boillat@hsr.ch",
                    StartDate = "HS2018"
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
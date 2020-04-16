using CoastlineServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoastlineServer.DAL.Configurations
{
    public class UserTypeConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.RowVersion).IsRowVersion();
            builder.Property(e => e.Biography).HasColumnType("VARCHAR(140)");
            builder.Property(e => e.StartDate).HasColumnType("VARCHAR(4)");
            builder.Property(e => e.FirstName).HasColumnType("VARCHAR(20)");
            builder.Property(e => e.LastName).HasColumnType("VARCHAR(20)");

            builder.HasData(
                new User()
                {
                    Id = -1,
                    FirstName = "David",
                    LastName = "Luthiger",
                    DegreeProgram = "Informatik",
                    Biography = "Start HS18",
                    Email = "david.luthiger@hsr.ch",
                    StartDate = "HS18"
                },
                new User()
                {
                    Id = -2,
                    FirstName = "Fabian",
                    LastName = "Germann",
                    DegreeProgram = "Informatik",
                    Biography = "Start HS2018",
                    Email = "fabian.germann@hsr.ch",
                    StartDate = "HS18"
                },
                new User()
                {
                    Id = -3,
                    FirstName = "Eliane",
                    LastName = "Schmidli",
                    DegreeProgram = "Informatik",
                    Biography = "Start HS2018",
                    Email = "eliane.schmidli@hsr.ch",
                    StartDate = "HS18"
                },
                new User()
                {
                    Id = -4,
                    FirstName = "Yves",
                    LastName = "Boillat",
                    DegreeProgram = "Informatik",
                    Biography = "Start HS2018",
                    Email = "yves.boillat@hsr.ch",
                    StartDate = "HS18"
                });
        }
    }
}
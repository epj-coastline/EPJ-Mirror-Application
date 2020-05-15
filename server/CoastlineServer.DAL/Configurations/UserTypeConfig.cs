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
            builder.Property(e => e.RowVersion).IsRowVersion();
            builder.Property(e => e.Biography).HasColumnType("VARCHAR(140)");
            builder.Property(e => e.StartDate).HasColumnType("VARCHAR(4)");
            builder.Property(e => e.FirstName).HasColumnType("VARCHAR(20)");
            builder.Property(e => e.LastName).HasColumnType("VARCHAR(20)");

            builder.HasData(
                new User()
                {
                    Id = "1fo9wW1Ul6I",
                    FirstName = "Mathias",
                    LastName = "Müller",
                    DegreeProgram = "Informatik",
                    Biography = "Start HS18",
                    Email = "mathias.mueller@hsr.ch",
                    StartDate = "HS18"
                },
                new User()
                {
                    Id = "2GqPPUoB4R7",
                    FirstName = "Tanja",
                    LastName = "Zurbriggen",
                    DegreeProgram = "Informatik",
                    Biography = "Start HS2018",
                    Email = "tanja.zurbriggen@hsr.ch",
                    StartDate = "HS18"
                },
                new User()
                {
                    Id = "3bPWlzE5nx1",
                    FirstName = "Tom",
                    LastName = "Eisenhauer",
                    DegreeProgram = "Informatik",
                    Biography = "Start HS2018",
                    Email = "tom.eisenhauer@hsr.ch",
                    StartDate = "HS18"
                },
                new User()
                {
                    Id = "4mNQjXctF0q",
                    FirstName = "Vanessa",
                    LastName = "Becker",
                    DegreeProgram = "Informatik",
                    Biography = "Start HS2018",
                    Email = "vanessa.becker@hsr.ch",
                    StartDate = "HS18"
                });
        }
    }
}
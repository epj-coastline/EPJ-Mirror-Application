using CoastlineServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoastlineServer.DAL.Configurations
{
    public class ModuleTypeConfig : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("Modules").HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.RowVersion).IsRowVersion();
            builder.Property(e => e.Name).HasColumnType("VARCHAR(60)");
            builder.Property(e => e.Token).HasColumnType("VARCHAR(10)");
            builder.Property(e => e.Responsibility).HasColumnType("VARCHAR(20)");

            builder.HasData(
                new Module()
                {
                    Id = -1,
                    Name = "Analysis 2 für Informatiker",
                    Responsibility = "Informatik",
                    Token = "An2I"
                },
                new Module()
                {
                    Id = -2,
                    Name = "Algorithmen und Datenstrukturen 1",
                    Responsibility = "Informatik",
                    Token = "AD1"
                },
                new Module()
                {
                    Id = -3,
                    Name = ".NET Technologien",
                    Responsibility = "Informatik",
                    Token = "MsTe"
                },
                new Module()
                {
                    Id = -4,
                    Name = "C++",
                    Responsibility = "Informatik",
                    Token = "Cpp"
                }
            );
        }
    }
}
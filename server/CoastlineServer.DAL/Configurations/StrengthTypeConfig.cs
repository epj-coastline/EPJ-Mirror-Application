using CoastlineServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoastlineServer.DAL.Configurations
{
    public class StrengthTypeConfig : IEntityTypeConfiguration<Strength>
    {
        public void Configure(EntityTypeBuilder<Strength> builder)
        {
            builder.ToTable("Strengths").HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.RowVersion).IsRowVersion();
            builder.HasOne(e => e.User)
                .WithMany(e => e.Strengths)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("FK_Strengths_UserId");
            builder.HasOne(e => e.Module)
                .WithMany(e => e.Strengths)
                .HasForeignKey(e => e.ModuleId)
                .HasConstraintName("FK_Strengths_ModuleId");

            builder.HasData(
                new Strength()
                {
                    Id = -1,
                    UserId = "1fo9wW1Ul6I",
                    ModuleId = -1
                },
                new Strength()
                {
                    Id = -2,
                    UserId = "2GqPPUoB4R7",
                    ModuleId = -2
                },
                new Strength()
                {
                    Id = -3,
                    UserId = "3bPWlzE5nx1",
                    ModuleId = -3
                }
            );
        }
    }
}
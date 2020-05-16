using CoastlineServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoastlineServer.DAL.Configurations
{
    public class ConfirmationTypeConfig : IEntityTypeConfiguration<Confirmation>
    {
        public void Configure(EntityTypeBuilder<Confirmation> builder)
        {
            builder.ToTable("Confirmations").HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.RowVersion).IsRowVersion();
            builder.HasOne(e => e.User)
                .WithMany(e => e.Confirmations)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("FK_Confirmations_UserId");
            builder.HasOne(e => e.Strength)
                .WithMany(e => e.Confirmations)
                .HasForeignKey(e => e.StrengthId)
                .HasConstraintName("FK_Confirmations_StrengthId");

            builder.HasData(
                new Confirmation()
                {
                    Id = -1,
                    StrengthId = -2,
                    UserId = "3bPWlzE5nx1"
                },
                new Confirmation()
                {
                    Id = -2,
                    StrengthId = -3,
                    UserId = "3bPWlzE5nx1"
                },
                new Confirmation()
                {
                    Id = -3,
                    StrengthId = -1,
                    UserId = "3bPWlzE5nx1"
                }
            );
        }
    }
}
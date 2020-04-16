using CoastlineServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoastlineServer.DAL.Configurations
{
    public class StudyGroupTypeConfig : IEntityTypeConfiguration<StudyGroup>
    {
        public void Configure(EntityTypeBuilder<StudyGroup> builder)
        {
            builder.ToTable("StudyGroups").HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.RowVersion).IsRowVersion();
            builder.Property(e => e.Purpose).HasColumnType("VARCHAR(40)");
            builder.HasOne(e => e.User)
                .WithMany(e => e.StudyGroups)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("FK_StudyGroups_UserId");
        }
    }
}
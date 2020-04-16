using CoastlineServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoastlineServer.DAL.Context
{
    public class MemberTypeConfig : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Members").HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.RowVersion).IsRowVersion();
            builder.HasOne(e => e.User)
                .WithMany(e => e.Members)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("FK_Members_UserId");
            builder.HasOne(e => e.StudyGroup)
                .WithMany(e => e.Members)
                .HasForeignKey(e => e.StudyGroupId)
                .HasConstraintName("FK_Members_StudyGroupId");
        }
    }
}
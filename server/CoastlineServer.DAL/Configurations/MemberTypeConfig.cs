using System;
using CoastlineServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoastlineServer.DAL.Configurations
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

            builder.HasData(
                new Member()
                {
                    Id = -1,
                    AccessionDate = new DateTime(2020, 03, 11, 18, 22, 50),
                    UserId = "1fo9wW1Ul6I",
                    StudyGroupId = -1
                },
                new Member()
                {
                    Id = -2,
                    AccessionDate = new DateTime(2020, 03, 11, 18, 22, 50),
                    UserId = "2GqPPUoB4R7",
                    StudyGroupId = -2
                },
                new Member()
                {
                    Id = -3,
                    AccessionDate = new DateTime(2020, 03, 11, 18, 22, 50),
                    UserId = "3bPWlzE5nx1",
                    StudyGroupId = -4
                },
                new Member()
                {
                    Id = -4,
                    AccessionDate = new DateTime(2020, 03, 11, 18, 22, 50),
                    UserId = "4mNQjXctF0q",
                    StudyGroupId = -4
                },
                new Member()
                {
                    Id = -5,
                    AccessionDate = new DateTime(2020, 03, 11, 18, 22, 50),
                    UserId = "4mNQjXctF0q",
                    StudyGroupId = -5
                },
                new Member()
                {
                    Id = -6,
                    AccessionDate = new DateTime(2020, 03, 11, 18, 22, 50),
                    UserId = "1fo9wW1Ul6I",
                    StudyGroupId = -3
                }
            );
        }
    }
}
using System;
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
            builder.Property(e => e.Purpose).HasColumnType("VARCHAR(140)");
            builder.HasOne(e => e.User)
                .WithMany(e => e.StudyGroups)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("FK_StudyGroups_UserId");
            builder.HasOne(e => e.Module)
                .WithMany(e => e.StudyGroups)
                .HasForeignKey(e => e.ModuleId)
                .HasConstraintName("FK_StudyGroups_ModuleId");

            builder.HasData(
                new StudyGroup()
                {
                    Id = -1,
                    Purpose = "Integrale An2I",
                    CreationDate = new DateTime(2020, 1, 1),
                    UserId = "1",
                    ModuleId = -1
                },
                new StudyGroup()
                {
                    Id = -2,
                    Purpose = "Rekursion AD1",
                    CreationDate = new DateTime(2020, 2, 16),
                    UserId = "2",
                    ModuleId = -2
                },
                new StudyGroup()
                {
                    Id = -3,
                    Purpose = "EF Core MsTe",
                    CreationDate = new DateTime(2020, 3, 13),
                    UserId = "3",
                    ModuleId = -3
                },
                new StudyGroup()
                {
                    Id = -4,
                    Purpose = "Tests schreiben C++",
                    CreationDate = new DateTime(2020, 4, 3),
                    UserId = "4",
                    ModuleId = -4
                },
                new StudyGroup()
                {
                    Id = -5,
                    Purpose = "Algorithmen in C++",
                    CreationDate = new DateTime(2020, 4, 16),
                    UserId = "4",
                    ModuleId = -4
                }
            );
        }
    }
}
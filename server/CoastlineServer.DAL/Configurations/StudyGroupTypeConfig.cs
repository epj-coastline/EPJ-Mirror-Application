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
                    Purpose = "Integral lösen mit Wolfram Alpha",
                    CreationDate = new DateTime(2020, 1, 1),
                    UserId = "1fo9wW1Ul6I",
                    ModuleId = -1
                },
                new StudyGroup()
                {
                    Id = -2,
                    Purpose = "Möchtest jemand das Thema Rekursion aus verganger Übung vertiefen?",
                    CreationDate = new DateTime(2020, 2, 16),
                    UserId = "2GqPPUoB4R7",
                    ModuleId = -2
                },
                new StudyGroup()
                {
                    Id = -3,
                    Purpose = "Austausch zum Code-First Ansatz mit EF Core für das MsTe-Testat",
                    CreationDate = new DateTime(2020, 3, 13),
                    UserId = "3bPWlzE5nx1",
                    ModuleId = -3
                },
                new StudyGroup()
                {
                    Id = -4,
                    Purpose =
                        "Ich würde gerne die Prüfung 2018 besprechen, da es keine Musterlösung gibt. Hat jemand Interesse?",
                    CreationDate = new DateTime(2020, 4, 3),
                    UserId = "4mNQjXctF0q",
                    ModuleId = -4
                },
                new StudyGroup()
                {
                    Id = -5,
                    Purpose = "Hat jemand lust, ein paar Aufgaben zum Thema Dynamic Dispatching zu lösen?",
                    CreationDate = new DateTime(2020, 4, 16),
                    UserId = "3bPWlzE5nx1",
                    ModuleId = -4
                }
            );
        }
    }
}
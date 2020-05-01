﻿// <auto-generated />
using System;
using CoastlineServer.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CoastlineServer.DAL.Migrations
{
    [DbContext(typeof(CoastlineContext))]
    [Migration("20200430061317_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("CoastlineServer.DAL.Entities.Confirmation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<int>("StrengthId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StrengthId");

                    b.HasIndex("UserId");

                    b.ToTable("Confirmations");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            StrengthId = -2,
                            UserId = -1
                        },
                        new
                        {
                            Id = -2,
                            StrengthId = -3,
                            UserId = -2
                        },
                        new
                        {
                            Id = -3,
                            StrengthId = -1,
                            UserId = -3
                        });
                });

            modelBuilder.Entity("CoastlineServer.DAL.Entities.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("AccessionDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<int>("StudyGroupId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StudyGroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Members");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            AccessionDate = new DateTime(2020, 3, 11, 18, 22, 50, 0, DateTimeKind.Unspecified),
                            StudyGroupId = -1,
                            UserId = -1
                        },
                        new
                        {
                            Id = -2,
                            AccessionDate = new DateTime(2020, 3, 11, 18, 22, 50, 0, DateTimeKind.Unspecified),
                            StudyGroupId = -2,
                            UserId = -2
                        },
                        new
                        {
                            Id = -3,
                            AccessionDate = new DateTime(2020, 3, 11, 18, 22, 50, 0, DateTimeKind.Unspecified),
                            StudyGroupId = -4,
                            UserId = -3
                        },
                        new
                        {
                            Id = -4,
                            AccessionDate = new DateTime(2020, 3, 11, 18, 22, 50, 0, DateTimeKind.Unspecified),
                            StudyGroupId = -4,
                            UserId = -4
                        },
                        new
                        {
                            Id = -5,
                            AccessionDate = new DateTime(2020, 3, 11, 18, 22, 50, 0, DateTimeKind.Unspecified),
                            StudyGroupId = -5,
                            UserId = -4
                        },
                        new
                        {
                            Id = -6,
                            AccessionDate = new DateTime(2020, 3, 11, 18, 22, 50, 0, DateTimeKind.Unspecified),
                            StudyGroupId = -3,
                            UserId = -1
                        });
                });

            modelBuilder.Entity("CoastlineServer.DAL.Entities.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("VARCHAR(60)");

                    b.Property<string>("Responsibility")
                        .HasColumnType("VARCHAR(20)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<string>("Token")
                        .HasColumnType("VARCHAR(10)");

                    b.HasKey("Id");

                    b.ToTable("Modules");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Name = "Analysis 2 für Informatiker",
                            Responsibility = "Informatik",
                            Token = "An2I"
                        },
                        new
                        {
                            Id = -2,
                            Name = "Algorithmen und Datenstrukturen 1",
                            Responsibility = "Informatik",
                            Token = "AD1"
                        },
                        new
                        {
                            Id = -3,
                            Name = ".NET Technologien",
                            Responsibility = "Informatik",
                            Token = "MsTe"
                        },
                        new
                        {
                            Id = -4,
                            Name = "C++",
                            Responsibility = "Informatik",
                            Token = "Cpp"
                        });
                });

            modelBuilder.Entity("CoastlineServer.DAL.Entities.Strength", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ModuleId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.HasIndex("UserId");

                    b.ToTable("Strengths");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            ModuleId = -1,
                            UserId = -1
                        },
                        new
                        {
                            Id = -2,
                            ModuleId = -2,
                            UserId = -2
                        },
                        new
                        {
                            Id = -3,
                            ModuleId = -3,
                            UserId = -3
                        });
                });

            modelBuilder.Entity("CoastlineServer.DAL.Entities.StudyGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ModuleId")
                        .HasColumnType("integer");

                    b.Property<string>("Purpose")
                        .HasColumnType("VARCHAR(40)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.HasIndex("UserId");

                    b.ToTable("StudyGroups");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            CreationDate = new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModuleId = -1,
                            Purpose = "Integrale An2I",
                            UserId = -1
                        },
                        new
                        {
                            Id = -2,
                            CreationDate = new DateTime(2020, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModuleId = -2,
                            Purpose = "Rekursion AD1",
                            UserId = -2
                        },
                        new
                        {
                            Id = -3,
                            CreationDate = new DateTime(2020, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModuleId = -3,
                            Purpose = "EF Core MsTe",
                            UserId = -3
                        },
                        new
                        {
                            Id = -4,
                            CreationDate = new DateTime(2020, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModuleId = -4,
                            Purpose = "Tests schreiben C++",
                            UserId = -4
                        },
                        new
                        {
                            Id = -5,
                            CreationDate = new DateTime(2020, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModuleId = -4,
                            Purpose = "Algorithmen in C++",
                            UserId = -4
                        });
                });

            modelBuilder.Entity("CoastlineServer.DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Biography")
                        .HasColumnType("VARCHAR(140)");

                    b.Property<string>("DegreeProgram")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("VARCHAR(20)");

                    b.Property<string>("LastName")
                        .HasColumnType("VARCHAR(20)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<string>("StartDate")
                        .HasColumnType("VARCHAR(4)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Biography = "Start HS18",
                            DegreeProgram = "Informatik",
                            Email = "david.luthiger@hsr.ch",
                            FirstName = "David",
                            LastName = "Luthiger",
                            StartDate = "HS18"
                        },
                        new
                        {
                            Id = -2,
                            Biography = "Start HS2018",
                            DegreeProgram = "Informatik",
                            Email = "fabian.germann@hsr.ch",
                            FirstName = "Fabian",
                            LastName = "Germann",
                            StartDate = "HS18"
                        },
                        new
                        {
                            Id = -3,
                            Biography = "Start HS2018",
                            DegreeProgram = "Informatik",
                            Email = "eliane.schmidli@hsr.ch",
                            FirstName = "Eliane",
                            LastName = "Schmidli",
                            StartDate = "HS18"
                        },
                        new
                        {
                            Id = -4,
                            Biography = "Start HS2018",
                            DegreeProgram = "Informatik",
                            Email = "yves.boillat@hsr.ch",
                            FirstName = "Yves",
                            LastName = "Boillat",
                            StartDate = "HS18"
                        });
                });

            modelBuilder.Entity("CoastlineServer.DAL.Entities.Confirmation", b =>
                {
                    b.HasOne("CoastlineServer.DAL.Entities.Strength", "Strength")
                        .WithMany("Confirmations")
                        .HasForeignKey("StrengthId")
                        .HasConstraintName("FK_Confirmations_StrengthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoastlineServer.DAL.Entities.User", "User")
                        .WithMany("Confirmations")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Confirmations_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoastlineServer.DAL.Entities.Member", b =>
                {
                    b.HasOne("CoastlineServer.DAL.Entities.StudyGroup", "StudyGroup")
                        .WithMany("Members")
                        .HasForeignKey("StudyGroupId")
                        .HasConstraintName("FK_Members_StudyGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoastlineServer.DAL.Entities.User", "User")
                        .WithMany("Members")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Members_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoastlineServer.DAL.Entities.Strength", b =>
                {
                    b.HasOne("CoastlineServer.DAL.Entities.Module", "Module")
                        .WithMany("Strengths")
                        .HasForeignKey("ModuleId")
                        .HasConstraintName("FK_Strengths_ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoastlineServer.DAL.Entities.User", "User")
                        .WithMany("Strengths")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Strengths_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoastlineServer.DAL.Entities.StudyGroup", b =>
                {
                    b.HasOne("CoastlineServer.DAL.Entities.Module", "Module")
                        .WithMany("StudyGroups")
                        .HasForeignKey("ModuleId")
                        .HasConstraintName("FK_StudyGroups_ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoastlineServer.DAL.Entities.User", "User")
                        .WithMany("StudyGroups")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_StudyGroups_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

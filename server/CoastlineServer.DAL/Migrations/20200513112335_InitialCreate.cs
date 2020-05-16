﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CoastlineServer.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "VARCHAR(10)", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(60)", nullable: true),
                    Responsibility = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    LastName = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Biography = table.Column<string>(type: "VARCHAR(140)", nullable: true),
                    DegreeProgram = table.Column<string>(nullable: true),
                    StartDate = table.Column<string>(type: "VARCHAR(4)", nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Strengths",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    ModuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strengths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Strengths_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Strengths_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Purpose = table.Column<string>(type: "VARCHAR(140)", nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    ModuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyGroups_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyGroups_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Confirmations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    StrengthId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Confirmations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Confirmations_StrengthId",
                        column: x => x.StrengthId,
                        principalTable: "Strengths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Confirmations_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccessionDate = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StudyGroupId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_StudyGroupId",
                        column: x => x.StudyGroupId,
                        principalTable: "StudyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Members_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "Name", "Responsibility", "Token" },
                values: new object[,]
                {
                    { -1, "Analysis 2 für Informatiker", "Informatik", "An2I" },
                    { -2, "Algorithmen und Datenstrukturen 1", "Informatik", "AD1" },
                    { -3, ".NET Technologien", "Informatik", "MsTe" },
                    { -4, "C++", "Informatik", "Cpp" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Biography", "DegreeProgram", "Email", "FirstName", "LastName", "StartDate" },
                values: new object[,]
                {
                    { -1, "Start HS18", "Informatik", "david.luthiger@hsr.ch", "David", "Luthiger", "HS18" },
                    { -2, "Start HS2018", "Informatik", "fabian.germann@hsr.ch", "Fabian", "Germann", "HS18" },
                    { -3, "Start HS2018", "Informatik", "eliane.schmidli@hsr.ch", "Eliane", "Schmidli", "HS18" },
                    { -4, "Start HS2018", "Informatik", "yves.boillat@hsr.ch", "Yves", "Boillat", "HS18" }
                });

            migrationBuilder.InsertData(
                table: "Strengths",
                columns: new[] { "Id", "ModuleId", "UserId" },
                values: new object[,]
                {
                    { -1, -1, -1 },
                    { -2, -2, -2 },
                    { -3, -3, -3 }
                });

            migrationBuilder.InsertData(
                table: "StudyGroups",
                columns: new[] { "Id", "CreationDate", "ModuleId", "Purpose", "UserId" },
                values: new object[,]
                {
                    { -1, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), -1, "Integrale An2I", -1 },
                    { -2, new DateTime(2020, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), -2, "Rekursion AD1", -2 },
                    { -3, new DateTime(2020, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), -3, "EF Core MsTe", -3 },
                    { -4, new DateTime(2020, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), -4, "Tests schreiben C++", -4 },
                    { -5, new DateTime(2020, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), -4, "Algorithmen in C++", -4 }
                });

            migrationBuilder.InsertData(
                table: "Confirmations",
                columns: new[] { "Id", "StrengthId", "UserId" },
                values: new object[,]
                {
                    { -3, -1, -3 },
                    { -1, -2, -1 },
                    { -2, -3, -2 }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "AccessionDate", "StudyGroupId", "UserId" },
                values: new object[,]
                {
                    { -1, new DateTime(2020, 3, 11, 18, 22, 50, 0, DateTimeKind.Unspecified), -1, -1 },
                    { -2, new DateTime(2020, 3, 11, 18, 22, 50, 0, DateTimeKind.Unspecified), -2, -2 },
                    { -6, new DateTime(2020, 3, 11, 18, 22, 50, 0, DateTimeKind.Unspecified), -3, -1 },
                    { -3, new DateTime(2020, 3, 11, 18, 22, 50, 0, DateTimeKind.Unspecified), -4, -3 },
                    { -4, new DateTime(2020, 3, 11, 18, 22, 50, 0, DateTimeKind.Unspecified), -4, -4 },
                    { -5, new DateTime(2020, 3, 11, 18, 22, 50, 0, DateTimeKind.Unspecified), -5, -4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Confirmations_StrengthId",
                table: "Confirmations",
                column: "StrengthId");

            migrationBuilder.CreateIndex(
                name: "IX_Confirmations_UserId",
                table: "Confirmations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_StudyGroupId",
                table: "Members",
                column: "StudyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_UserId",
                table: "Members",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Strengths_ModuleId",
                table: "Strengths",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Strengths_UserId",
                table: "Strengths",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroups_ModuleId",
                table: "StudyGroups",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroups_UserId",
                table: "StudyGroups",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Confirmations");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Strengths");

            migrationBuilder.DropTable(
                name: "StudyGroups");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

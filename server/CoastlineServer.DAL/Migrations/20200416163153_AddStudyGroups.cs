using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CoastlineServer.DAL.Migrations
{
    public partial class AddStudyGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StartDate",
                table: "Users",
                type: "VARCHAR(4)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(6)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "StudyGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Purpose = table.Column<string>(type: "VARCHAR(40)", nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyGroups_UserId",
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
                table: "StudyGroups",
                columns: new[] { "Id", "CreationDate", "Purpose", "UserId" },
                values: new object[,]
                {
                    { -1, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Integrale An2I", -1 },
                    { -2, new DateTime(2020, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rekursion AD1", -2 },
                    { -3, new DateTime(2020, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "EF Core MsTe", -3 },
                    { -4, new DateTime(2020, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tests schreiben C++", -4 },
                    { -5, new DateTime(2020, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Algorithmen in C++", -4 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -4,
                column: "StartDate",
                value: "HS18");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -3,
                column: "StartDate",
                value: "HS18");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2,
                column: "StartDate",
                value: "HS18");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "Biography", "StartDate" },
                values: new object[] { "Start HS18", "HS18" });

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
                name: "IX_Members_StudyGroupId",
                table: "Members",
                column: "StudyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_UserId",
                table: "Members",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroups_UserId",
                table: "StudyGroups",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "StudyGroups");

            migrationBuilder.AlterColumn<string>(
                name: "StartDate",
                table: "Users",
                type: "VARCHAR(6)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(4)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -4,
                column: "StartDate",
                value: "HS18");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -3,
                column: "StartDate",
                value: "HS18");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2,
                column: "StartDate",
                value: "HS18");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "Biography", "StartDate" },
                values: new object[] { "Start HS18", "HS18" });
        }
    }
}

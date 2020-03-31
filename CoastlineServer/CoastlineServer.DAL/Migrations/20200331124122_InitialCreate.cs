using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CoastlineServer.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    StartDate = table.Column<string>(type: "VARCHAR(6)", nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Biography", "DegreeProgram", "Email", "FirstName", "LastName", "StartDate" },
                values: new object[,]
                {
                    { -1, "Start HS2018", "Informatik", "david.luthiger@hsr.ch", "David", "Luthiger", "HS2018" },
                    { -2, "Start HS2018", "Informatik", "fabian.germann@hsr.ch", "Fabian", "Germann", "HS2018" },
                    { -3, "Start HS2018", "Informatik", "eliane.schmidli@hsr.ch", "Eliane", "Schmidli", "HS2018" },
                    { -4, "Start HS2018", "Informatik", "yves.boillat@hsr.ch", "Yves", "Boillat", "HS2018" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

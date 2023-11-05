using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P01_StudentSystem.Migrations
{
    public partial class Added_Cource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cources",
                columns: table => new
                {
                    CourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Descriptoin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cources", x => x.CourceId);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    RosourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    ResourceType = table.Column<int>(type: "int", nullable: false),
                    CourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.RosourceId);
                    table.ForeignKey(
                        name: "FK_Resources_Cources_CourceId",
                        column: x => x.CourceId,
                        principalTable: "Cources",
                        principalColumn: "CourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CourceId",
                table: "Resources",
                column: "CourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Cources");
        }
    }
}

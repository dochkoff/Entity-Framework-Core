using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P01_StudentSystem.Migrations
{
    public partial class Added_Final2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentCourse",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourse", x => new { x.StudentId, x.CourceId });
                    table.ForeignKey(
                        name: "FK_StudentCourse_Cources_CourceId",
                        column: x => x.CourceId,
                        principalTable: "Cources",
                        principalColumn: "CourceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourse_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_CourceId",
                table: "StudentCourse",
                column: "CourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCourse");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P01_StudentSystem.Migrations
{
    public partial class Added_Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "CourceStudent",
                columns: table => new
                {
                    CourcesCourceId = table.Column<int>(type: "int", nullable: false),
                    StudentsStudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourceStudent", x => new { x.CourcesCourceId, x.StudentsStudentId });
                    table.ForeignKey(
                        name: "FK_CourceStudent_Cources_CourcesCourceId",
                        column: x => x.CourcesCourceId,
                        principalTable: "Cources",
                        principalColumn: "CourceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourceStudent_Student_StudentsStudentId",
                        column: x => x.StudentsStudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Homework",
                columns: table => new
                {
                    HomeworkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    ContentType = table.Column<int>(type: "int", nullable: false),
                    SubmissionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CourceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homework", x => x.HomeworkId);
                    table.ForeignKey(
                        name: "FK_Homework_Cources_CourceId",
                        column: x => x.CourceId,
                        principalTable: "Cources",
                        principalColumn: "CourceId");
                    table.ForeignKey(
                        name: "FK_Homework_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourceStudent_StudentsStudentId",
                table: "CourceStudent",
                column: "StudentsStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Homework_CourceId",
                table: "Homework",
                column: "CourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Homework_StudentId",
                table: "Homework",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourceStudent");

            migrationBuilder.DropTable(
                name: "Homework");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}

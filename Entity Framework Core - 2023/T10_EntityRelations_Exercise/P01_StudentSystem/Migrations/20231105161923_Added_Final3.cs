using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P01_StudentSystem.Migrations
{
    public partial class Added_Final3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homework_Cources_CourceId",
                table: "Homework");

            migrationBuilder.DropForeignKey(
                name: "FK_Homework_Student_StudentId",
                table: "Homework");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourse_Cources_CourceId",
                table: "StudentCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourse_Student_StudentId",
                table: "StudentCourse");

            migrationBuilder.DropTable(
                name: "CourceStudent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentCourse",
                table: "StudentCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Homework",
                table: "Homework");

            migrationBuilder.RenameTable(
                name: "StudentCourse",
                newName: "StudentsCourses");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "Homework",
                newName: "Homeworks");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourse_CourceId",
                table: "StudentsCourses",
                newName: "IX_StudentsCourses_CourceId");

            migrationBuilder.RenameIndex(
                name: "IX_Homework_StudentId",
                table: "Homeworks",
                newName: "IX_Homeworks_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Homework_CourceId",
                table: "Homeworks",
                newName: "IX_Homeworks_CourceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentsCourses",
                table: "StudentsCourses",
                columns: new[] { "StudentId", "CourceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Homeworks",
                table: "Homeworks",
                column: "HomeworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homeworks_Cources_CourceId",
                table: "Homeworks",
                column: "CourceId",
                principalTable: "Cources",
                principalColumn: "CourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homeworks_Students_StudentId",
                table: "Homeworks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsCourses_Cources_CourceId",
                table: "StudentsCourses",
                column: "CourceId",
                principalTable: "Cources",
                principalColumn: "CourceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsCourses_Students_StudentId",
                table: "StudentsCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homeworks_Cources_CourceId",
                table: "Homeworks");

            migrationBuilder.DropForeignKey(
                name: "FK_Homeworks_Students_StudentId",
                table: "Homeworks");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsCourses_Cources_CourceId",
                table: "StudentsCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsCourses_Students_StudentId",
                table: "StudentsCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentsCourses",
                table: "StudentsCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Homeworks",
                table: "Homeworks");

            migrationBuilder.RenameTable(
                name: "StudentsCourses",
                newName: "StudentCourse");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Student");

            migrationBuilder.RenameTable(
                name: "Homeworks",
                newName: "Homework");

            migrationBuilder.RenameIndex(
                name: "IX_StudentsCourses_CourceId",
                table: "StudentCourse",
                newName: "IX_StudentCourse_CourceId");

            migrationBuilder.RenameIndex(
                name: "IX_Homeworks_StudentId",
                table: "Homework",
                newName: "IX_Homework_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Homeworks_CourceId",
                table: "Homework",
                newName: "IX_Homework_CourceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentCourse",
                table: "StudentCourse",
                columns: new[] { "StudentId", "CourceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Homework",
                table: "Homework",
                column: "HomeworkId");

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

            migrationBuilder.CreateIndex(
                name: "IX_CourceStudent_StudentsStudentId",
                table: "CourceStudent",
                column: "StudentsStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homework_Cources_CourceId",
                table: "Homework",
                column: "CourceId",
                principalTable: "Cources",
                principalColumn: "CourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homework_Student_StudentId",
                table: "Homework",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourse_Cources_CourceId",
                table: "StudentCourse",
                column: "CourceId",
                principalTable: "Cources",
                principalColumn: "CourceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourse_Student_StudentId",
                table: "StudentCourse",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameMemberInCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembersInCourses_Courses_CourseId",
                table: "MembersInCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_MembersInCourses_Instructors_InstructorId",
                table: "MembersInCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MembersInCourses",
                table: "MembersInCourses");

            migrationBuilder.RenameTable(
                name: "MembersInCourses",
                newName: "InstructorInCourses");

            migrationBuilder.RenameIndex(
                name: "IX_MembersInCourses_InstructorId",
                table: "InstructorInCourses",
                newName: "IX_InstructorInCourses_InstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_MembersInCourses_CourseId",
                table: "InstructorInCourses",
                newName: "IX_InstructorInCourses_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstructorInCourses",
                table: "InstructorInCourses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorInCourses_Courses_CourseId",
                table: "InstructorInCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorInCourses_Instructors_InstructorId",
                table: "InstructorInCourses",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstructorInCourses_Courses_CourseId",
                table: "InstructorInCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructorInCourses_Instructors_InstructorId",
                table: "InstructorInCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstructorInCourses",
                table: "InstructorInCourses");

            migrationBuilder.RenameTable(
                name: "InstructorInCourses",
                newName: "MembersInCourses");

            migrationBuilder.RenameIndex(
                name: "IX_InstructorInCourses_InstructorId",
                table: "MembersInCourses",
                newName: "IX_MembersInCourses_InstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_InstructorInCourses_CourseId",
                table: "MembersInCourses",
                newName: "IX_MembersInCourses_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MembersInCourses",
                table: "MembersInCourses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MembersInCourses_Courses_CourseId",
                table: "MembersInCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MembersInCourses_Instructors_InstructorId",
                table: "MembersInCourses",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id");
        }
    }
}

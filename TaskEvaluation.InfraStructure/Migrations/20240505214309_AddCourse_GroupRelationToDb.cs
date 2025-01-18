using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskEvaluation.InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCourse_GroupRelationToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Groups_CourseId",
                table: "Groups",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Courses_CourseId",
                table: "Groups",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Courses_CourseId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CourseId",
                table: "Groups");
        }
    }
}

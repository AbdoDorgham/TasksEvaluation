using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskEvaluation.InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStudent_SolutionRelationToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Solutions_StudentId",
                table: "Solutions",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_Students_StudentId",
                table: "Solutions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Students_StudentId",
                table: "Solutions");

            migrationBuilder.DropIndex(
                name: "IX_Solutions_StudentId",
                table: "Solutions");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskEvaluation.InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSolution_AssignmentRelationToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Solutions_AssignmentId",
                table: "Solutions",
                column: "AssignmentId",
                unique: true,
                filter: "[AssignmentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_Assignments_AssignmentId",
                table: "Solutions",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Assignments_AssignmentId",
                table: "Solutions");

            migrationBuilder.DropIndex(
                name: "IX_Solutions_AssignmentId",
                table: "Solutions");
        }
    }
}

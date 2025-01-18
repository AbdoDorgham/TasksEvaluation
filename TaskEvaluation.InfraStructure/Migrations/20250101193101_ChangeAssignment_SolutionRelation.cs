using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskEvaluation.InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAssignment_SolutionRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Solutions_AssignmentId",
                table: "Solutions");

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_AssignmentId",
                table: "Solutions",
                column: "AssignmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Solutions_AssignmentId",
                table: "Solutions");

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_AssignmentId",
                table: "Solutions",
                column: "AssignmentId",
                unique: true,
                filter: "[AssignmentId] IS NOT NULL");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskEvaluation.InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSolutionGradeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Solutions_GradeId",
                table: "Solutions");

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_GradeId",
                table: "Solutions",
                column: "GradeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Solutions_GradeId",
                table: "Solutions");

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_GradeId",
                table: "Solutions",
                column: "GradeId",
                unique: true,
                filter: "[GradeId] IS NOT NULL");
        }
    }
}

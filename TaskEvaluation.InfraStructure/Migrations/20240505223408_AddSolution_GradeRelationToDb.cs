using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskEvaluation.InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSolution_GradeRelationToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Solutions_GradeId",
                table: "Solutions",
                column: "GradeId",
                unique: true,
                filter: "[GradeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_EvaluationGrades_GradeId",
                table: "Solutions",
                column: "GradeId",
                principalTable: "EvaluationGrades",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_EvaluationGrades_GradeId",
                table: "Solutions");

            migrationBuilder.DropIndex(
                name: "IX_Solutions_GradeId",
                table: "Solutions");
        }
    }
}

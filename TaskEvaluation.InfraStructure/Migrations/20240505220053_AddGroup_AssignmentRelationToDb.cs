using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskEvaluation.InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGroup_AssignmentRelationToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Assignments_GroupId",
                table: "Assignments",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Groups_GroupId",
                table: "Assignments",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Groups_GroupId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_GroupId",
                table: "Assignments");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymStudioOS.Migrations
{
    /// <inheritdoc />
    public partial class AddBranchIdToGymUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "GymUserRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GymUserRoles_BranchId",
                table: "GymUserRoles",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_GymUserRoles_GymBranches_BranchId",
                table: "GymUserRoles",
                column: "BranchId",
                principalTable: "GymBranches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GymUserRoles_GymBranches_BranchId",
                table: "GymUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_GymUserRoles_BranchId",
                table: "GymUserRoles");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "GymUserRoles");
        }
    }
}

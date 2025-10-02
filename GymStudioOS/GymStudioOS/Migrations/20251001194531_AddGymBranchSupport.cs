using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymStudioOS.Migrations
{
    /// <inheritdoc />
    public partial class AddGymBranchSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Gyms");

            migrationBuilder.CreateTable(
                name: "GymBranches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GymId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GymBranches_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GymBranches_GymId",
                table: "GymBranches",
                column: "GymId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GymBranches");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Gyms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

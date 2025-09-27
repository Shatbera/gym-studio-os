using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymStudioOS.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonalIdToUserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonalId",
                table: "UserProfiles",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalId",
                table: "UserProfiles");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS_API.Migrations
{
    /// <inheritdoc />
    public partial class updateprofileModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Profiles",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Profiles");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Profiles",
                newName: "Name");
        }
    }
}

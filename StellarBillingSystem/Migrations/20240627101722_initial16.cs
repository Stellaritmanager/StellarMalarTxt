using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Isdelete",
                table: "SHScreenMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Isdelete",
                table: "SHrollType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Isdelete",
                table: "SHrollaccess",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Isdelete",
                table: "SHRoleaccessModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "SHresourceType",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isdelete",
                table: "SHScreenMaster");

            migrationBuilder.DropColumn(
                name: "Isdelete",
                table: "SHrollType");

            migrationBuilder.DropColumn(
                name: "Isdelete",
                table: "SHrollaccess");

            migrationBuilder.DropColumn(
                name: "Isdelete",
                table: "SHRoleaccessModel");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "SHresourceType");
        }
    }
}

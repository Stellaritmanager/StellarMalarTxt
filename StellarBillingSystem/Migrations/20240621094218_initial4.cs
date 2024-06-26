using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedDate",
                table: "SHSignUp",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedUser",
                table: "SHSignUp",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedmachine",
                table: "SHSignUp",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "SHSignUp");

            migrationBuilder.DropColumn(
                name: "LastUpdatedUser",
                table: "SHSignUp");

            migrationBuilder.DropColumn(
                name: "LastUpdatedmachine",
                table: "SHSignUp");
        }
    }
}

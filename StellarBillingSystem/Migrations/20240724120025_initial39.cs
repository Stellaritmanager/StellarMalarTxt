using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial39 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedDate",
                table: "SHBranchMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lastUpdatedMachine",
                table: "SHBranchMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lastUpdatedUser",
                table: "SHBranchMaster",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "SHBranchMaster");

            migrationBuilder.DropColumn(
                name: "lastUpdatedMachine",
                table: "SHBranchMaster");

            migrationBuilder.DropColumn(
                name: "lastUpdatedUser",
                table: "SHBranchMaster");
        }
    }
}

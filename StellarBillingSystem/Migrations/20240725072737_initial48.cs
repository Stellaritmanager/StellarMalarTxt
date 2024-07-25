using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial48 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHScreenMaster",
                table: "SHScreenMaster");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHScreenMaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHScreenMaster",
                table: "SHScreenMaster",
                columns: new[] { "ScreenId", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHScreenMaster",
                table: "SHScreenMaster");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHScreenMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHScreenMaster",
                table: "SHScreenMaster",
                column: "ScreenId");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial49 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SBLogs",
                table: "SBLogs");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SBLogs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SBLogs",
                table: "SBLogs",
                columns: new[] { "LogID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SBLogs",
                table: "SBLogs");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SBLogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SBLogs",
                table: "SBLogs",
                column: "LogID");
        }
    }
}

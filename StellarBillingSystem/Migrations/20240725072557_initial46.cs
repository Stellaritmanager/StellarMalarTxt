using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial46 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHrollaccess",
                table: "SHrollaccess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SHRoleaccessModel",
                table: "SHRoleaccessModel");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHrollaccess",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHRoleaccessModel",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHrollaccess",
                table: "SHrollaccess",
                columns: new[] { "StaffID", "RollID", "BranchID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHRoleaccessModel",
                table: "SHRoleaccessModel",
                columns: new[] { "RollID", "ScreenID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHrollaccess",
                table: "SHrollaccess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SHRoleaccessModel",
                table: "SHRoleaccessModel");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHrollaccess",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHRoleaccessModel",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHrollaccess",
                table: "SHrollaccess",
                columns: new[] { "StaffID", "RollID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHRoleaccessModel",
                table: "SHRoleaccessModel",
                columns: new[] { "RollID", "ScreenID" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Isdelete",
                table: "SHScreenMaster",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "Isdelete",
                table: "SHrollType",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "Isdelete",
                table: "SHrollaccess",
                newName: "IsDelete");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "SHScreenMaster",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "SHrollType",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "SHrollaccess",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "SHScreenMaster",
                newName: "Isdelete");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "SHrollType",
                newName: "Isdelete");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "SHrollaccess",
                newName: "Isdelete");

            migrationBuilder.AlterColumn<string>(
                name: "Isdelete",
                table: "SHScreenMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Isdelete",
                table: "SHrollType",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Isdelete",
                table: "SHrollaccess",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}

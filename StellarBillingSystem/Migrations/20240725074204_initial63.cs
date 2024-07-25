using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial63 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHReedemHistory",
                table: "SHReedemHistory");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHReedemHistory",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHReedemHistory",
                table: "SHReedemHistory",
                columns: new[] { "CustomerNumber", "DateOfReedem", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHReedemHistory",
                table: "SHReedemHistory");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHReedemHistory",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHReedemHistory",
                table: "SHReedemHistory",
                columns: new[] { "CustomerNumber", "DateOfReedem" });
        }
    }
}

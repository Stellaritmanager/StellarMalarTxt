using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial54 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHVoucherDetails",
                table: "SHVoucherDetails");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHVoucherDetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHVoucherDetails",
                table: "SHVoucherDetails",
                columns: new[] { "VoucherID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHVoucherDetails",
                table: "SHVoucherDetails");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHVoucherDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHVoucherDetails",
                table: "SHVoucherDetails",
                column: "VoucherID");
        }
    }
}

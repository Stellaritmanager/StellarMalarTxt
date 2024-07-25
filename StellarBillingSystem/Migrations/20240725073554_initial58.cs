using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial58 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHVoucherMaster",
                table: "SHVoucherMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SHRackPartionProduct",
                table: "SHRackPartionProduct");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHVoucherMaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHRackPartionProduct",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHVoucherMaster",
                table: "SHVoucherMaster",
                columns: new[] { "VoucherID", "BranchID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHRackPartionProduct",
                table: "SHRackPartionProduct",
                columns: new[] { "PartitionID", "ProductID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHVoucherMaster",
                table: "SHVoucherMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SHRackPartionProduct",
                table: "SHRackPartionProduct");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHVoucherMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHRackPartionProduct",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHVoucherMaster",
                table: "SHVoucherMaster",
                column: "VoucherID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHRackPartionProduct",
                table: "SHRackPartionProduct",
                columns: new[] { "PartitionID", "ProductID" });
        }
    }
}

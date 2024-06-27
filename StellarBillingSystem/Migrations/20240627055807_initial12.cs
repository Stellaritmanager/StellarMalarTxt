using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHCustomerMaster",
                table: "SHCustomerMaster");

            migrationBuilder.DropColumn(
                name: "PointsReedem",
                table: "SHCustomerMaster");

            migrationBuilder.DropColumn(
                name: "VoucherDiscount",
                table: "SHCustomerMaster");

            migrationBuilder.DropColumn(
                name: "VoucherNumber",
                table: "SHCustomerMaster");

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "SHCustomerMaster",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerID",
                table: "SHCustomerMaster",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHCustomerMaster",
                table: "SHCustomerMaster",
                column: "MobileNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHCustomerMaster",
                table: "SHCustomerMaster");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerID",
                table: "SHCustomerMaster",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "SHCustomerMaster",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "PointsReedem",
                table: "SHCustomerMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VoucherDiscount",
                table: "SHCustomerMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VoucherNumber",
                table: "SHCustomerMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHCustomerMaster",
                table: "SHCustomerMaster",
                column: "CustomerID");
        }
    }
}

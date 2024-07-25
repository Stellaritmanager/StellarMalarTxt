using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial55 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHCustomerBilling",
                table: "SHCustomerBilling");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHCustomerBilling",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHCustomerBilling",
                table: "SHCustomerBilling",
                columns: new[] { "BillID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHCustomerBilling",
                table: "SHCustomerBilling");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHCustomerBilling",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHCustomerBilling",
                table: "SHCustomerBilling",
                column: "BillID");
        }
    }
}

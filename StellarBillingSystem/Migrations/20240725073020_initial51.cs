using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial51 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHCustomerMaster",
                table: "SHCustomerMaster");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHCustomerMaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHCustomerMaster",
                table: "SHCustomerMaster",
                columns: new[] { "MobileNumber", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHCustomerMaster",
                table: "SHCustomerMaster");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHCustomerMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHCustomerMaster",
                table: "SHCustomerMaster",
                column: "MobileNumber");
        }
    }
}

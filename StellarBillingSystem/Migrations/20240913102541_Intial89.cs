using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class Intial89 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHbilldetails",
                table: "SHbilldetails");

            migrationBuilder.AlterColumn<string>(
                name: "BillID",
                table: "SHbilldetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHbilldetails",
                table: "SHbilldetails",
                columns: new[] { "Id", "ProductID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHbilldetails",
                table: "SHbilldetails");

            migrationBuilder.AlterColumn<string>(
                name: "BillID",
                table: "SHbilldetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHbilldetails",
                table: "SHbilldetails",
                columns: new[] { "BillID", "ProductID", "BranchID" });
        }
    }
}

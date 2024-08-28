using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial75 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHGodown",
                table: "SHGodown");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierInformation",
                table: "SHGodown",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DatefofPurchase",
                table: "SHGodown",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHGodown",
                table: "SHGodown",
                columns: new[] { "ProductID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHGodown",
                table: "SHGodown");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierInformation",
                table: "SHGodown",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DatefofPurchase",
                table: "SHGodown",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHGodown",
                table: "SHGodown",
                columns: new[] { "ProductID", "DatefofPurchase", "SupplierInformation", "BranchID" });
        }
    }
}

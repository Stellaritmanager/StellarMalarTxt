using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SHGodown");

            migrationBuilder.DropColumn(
                name: "NumberofStocksinRack",
                table: "SHGodown");

            migrationBuilder.AddColumn<string>(
                name: "DatefofPurchase",
                table: "SHGodown",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SupplierInformation",
                table: "SHGodown",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatefofPurchase",
                table: "SHGodown");

            migrationBuilder.DropColumn(
                name: "SupplierInformation",
                table: "SHGodown");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SHGodown",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberofStocksinRack",
                table: "SHGodown",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

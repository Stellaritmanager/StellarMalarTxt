using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial35 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "SHProductMaster",
                newName: "SGST");

            migrationBuilder.AddColumn<string>(
                name: "CGST",
                table: "SHProductMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiscountCategory",
                table: "SHProductMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherTax",
                table: "SHProductMaster",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CGST",
                table: "SHProductMaster");

            migrationBuilder.DropColumn(
                name: "DiscountCategory",
                table: "SHProductMaster");

            migrationBuilder.DropColumn(
                name: "OtherTax",
                table: "SHProductMaster");

            migrationBuilder.RenameColumn(
                name: "SGST",
                table: "SHProductMaster",
                newName: "Discount");
        }
    }
}

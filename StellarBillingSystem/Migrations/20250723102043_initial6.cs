using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem_Malar.Migrations
{
    /// <inheritdoc />
    public partial class initial6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInwardModelMT",
                table: "ProductInwardModelMT");

            migrationBuilder.RenameTable(
                name: "ProductInwardModelMT",
                newName: "MTProductInward");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MTProductInward",
                table: "MTProductInward",
                columns: new[] { "InvoiceNumber", "ProductCode", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MTProductInward",
                table: "MTProductInward");

            migrationBuilder.RenameTable(
                name: "MTProductInward",
                newName: "ProductInwardModelMT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInwardModelMT",
                table: "ProductInwardModelMT",
                columns: new[] { "InvoiceNumber", "ProductCode", "BranchID" });
        }
    }
}

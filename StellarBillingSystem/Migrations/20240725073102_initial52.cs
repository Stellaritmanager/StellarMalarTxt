using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial52 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHDiscountCategory",
                table: "SHDiscountCategory");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHDiscountCategory",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHDiscountCategory",
                table: "SHDiscountCategory",
                columns: new[] { "CategoryID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHDiscountCategory",
                table: "SHDiscountCategory");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHDiscountCategory",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHDiscountCategory",
                table: "SHDiscountCategory",
                column: "CategoryID");
        }
    }
}

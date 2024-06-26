using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "SHDiscountCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "SHDiscountCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

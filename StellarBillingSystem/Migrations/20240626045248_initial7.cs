using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHDiscountCategory",
                table: "SHDiscountCategory");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "SHProductMaster",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "DiscountPrice",
                table: "SHDiscountCategory",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryID",
                table: "SHDiscountCategory",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHDiscountCategory",
                table: "SHDiscountCategory",
                column: "DiscountPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHDiscountCategory",
                table: "SHDiscountCategory");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "SHProductMaster");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryID",
                table: "SHDiscountCategory",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DiscountPrice",
                table: "SHDiscountCategory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHDiscountCategory",
                table: "SHDiscountCategory",
                column: "CategoryID");
        }
    }
}

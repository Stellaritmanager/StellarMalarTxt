using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHNetDiscountMaster",
                table: "SHNetDiscountMaster");

            migrationBuilder.AlterColumn<string>(
                name: "NetDiscount",
                table: "SHNetDiscountMaster",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHNetDiscountMaster",
                table: "SHNetDiscountMaster",
                column: "NetID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHNetDiscountMaster",
                table: "SHNetDiscountMaster");

            migrationBuilder.AlterColumn<string>(
                name: "NetDiscount",
                table: "SHNetDiscountMaster",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHNetDiscountMaster",
                table: "SHNetDiscountMaster",
                columns: new[] { "NetID", "NetDiscount" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHNetDiscountMaster",
                table: "SHNetDiscountMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SHDiscountCategory",
                table: "SHDiscountCategory");

            migrationBuilder.DropColumn(
                name: "NetDiscountID",
                table: "SHNetDiscountMaster");

            migrationBuilder.AddColumn<string>(
                name: "NetPoints",
                table: "SHPointsMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NetDiscount",
                table: "SHNetDiscountMaster",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
                name: "PK_SHNetDiscountMaster",
                table: "SHNetDiscountMaster",
                column: "NetDiscount");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHDiscountCategory",
                table: "SHDiscountCategory",
                column: "DiscountPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHNetDiscountMaster",
                table: "SHNetDiscountMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SHDiscountCategory",
                table: "SHDiscountCategory");

            migrationBuilder.DropColumn(
                name: "NetPoints",
                table: "SHPointsMaster");

            migrationBuilder.AlterColumn<string>(
                name: "NetDiscount",
                table: "SHNetDiscountMaster",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "NetDiscountID",
                table: "SHNetDiscountMaster",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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
                name: "PK_SHNetDiscountMaster",
                table: "SHNetDiscountMaster",
                column: "NetDiscountID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHDiscountCategory",
                table: "SHDiscountCategory",
                column: "CategoryID");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial57 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHNetDiscountMaster",
                table: "SHNetDiscountMaster");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHNetDiscountMaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHNetDiscountMaster",
                table: "SHNetDiscountMaster",
                columns: new[] { "NetID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHNetDiscountMaster",
                table: "SHNetDiscountMaster");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHNetDiscountMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHNetDiscountMaster",
                table: "SHNetDiscountMaster",
                column: "NetID");
        }
    }
}

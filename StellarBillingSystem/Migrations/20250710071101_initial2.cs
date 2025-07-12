using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem_skj.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHCategoryMaster",
                table: "SHCategoryMaster");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "SHCategoryMaster",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarketRate",
                table: "SHCategoryMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHCategoryMaster",
                table: "SHCategoryMaster",
                columns: new[] { "Id", "BranchID", "CategoryName" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHCategoryMaster",
                table: "SHCategoryMaster");

            migrationBuilder.DropColumn(
                name: "MarketRate",
                table: "SHCategoryMaster");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "SHCategoryMaster",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHCategoryMaster",
                table: "SHCategoryMaster",
                columns: new[] { "Id", "BranchID" });
        }
    }
}

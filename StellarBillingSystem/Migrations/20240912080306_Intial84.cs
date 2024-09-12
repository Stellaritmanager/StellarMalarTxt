using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class Intial84 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHCategoryMaster",
                table: "SHCategoryMaster");

            migrationBuilder.DropColumn(
                name: "CatID",
                table: "SHCategoryMaster");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryID",
                table: "SHCategoryMaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHCategoryMaster",
                table: "SHCategoryMaster",
                columns: new[] { "CategoryID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHCategoryMaster",
                table: "SHCategoryMaster");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryID",
                table: "SHCategoryMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<long>(
                name: "CatID",
                table: "SHCategoryMaster",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHCategoryMaster",
                table: "SHCategoryMaster",
                columns: new[] { "CatID", "BranchID" });
        }
    }
}

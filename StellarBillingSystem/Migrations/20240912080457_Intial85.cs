using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class Intial85 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Id",
                table: "SHCategoryMaster",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "101, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHCategoryMaster",
                table: "SHCategoryMaster",
                columns: new[] { "Id", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHCategoryMaster",
                table: "SHCategoryMaster");

            migrationBuilder.DropColumn(
                name: "Id",
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
    }
}

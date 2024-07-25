using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial41 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHCategoryMaster",
                table: "SHCategoryMaster");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHCategoryMaster",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHCategoryMaster");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHCategoryMaster",
                table: "SHCategoryMaster",
                column: "CategoryID");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial50 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHProductMaster",
                table: "SHProductMaster");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHProductMaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHProductMaster",
                table: "SHProductMaster",
                columns: new[] { "ProductID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHProductMaster",
                table: "SHProductMaster");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHProductMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHProductMaster",
                table: "SHProductMaster",
                column: "ProductID");
        }
    }
}

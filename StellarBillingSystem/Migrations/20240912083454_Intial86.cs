using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class Intial86 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHProductMaster",
                table: "SHProductMaster");

            migrationBuilder.AlterColumn<string>(
                name: "ProductID",
                table: "SHProductMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "SHProductMaster",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "101, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHProductMaster",
                table: "SHProductMaster",
                columns: new[] { "Id", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHProductMaster",
                table: "SHProductMaster");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SHProductMaster");

            migrationBuilder.AlterColumn<string>(
                name: "ProductID",
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
    }
}

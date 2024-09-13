using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class Intial87 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHbillmaster",
                table: "SHbillmaster");

            migrationBuilder.AlterColumn<string>(
                name: "BillID",
                table: "SHbillmaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "SHbillmaster",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "101, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHbillmaster",
                table: "SHbillmaster",
                columns: new[] { "Id", "BillDate", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHbillmaster",
                table: "SHbillmaster");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SHbillmaster");

            migrationBuilder.AlterColumn<string>(
                name: "BillID",
                table: "SHbillmaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHbillmaster",
                table: "SHbillmaster",
                columns: new[] { "BillID", "BillDate", "BranchID" });
        }
    }
}

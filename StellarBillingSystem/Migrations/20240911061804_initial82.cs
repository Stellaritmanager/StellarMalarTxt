using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial82 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHBillingPoints",
                table: "SHBillingPoints");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHBillingPoints",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHBillingPoints",
                table: "SHBillingPoints",
                columns: new[] { "BillID", "CustomerNumber", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHBillingPoints",
                table: "SHBillingPoints");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHBillingPoints");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHBillingPoints",
                table: "SHBillingPoints",
                columns: new[] { "BillID", "CustomerNumber" });
        }
    }
}

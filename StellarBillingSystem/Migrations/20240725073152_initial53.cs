using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial53 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHGSTMaster",
                table: "SHGSTMaster");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHGSTMaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHGSTMaster",
                table: "SHGSTMaster",
                columns: new[] { "TaxID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHGSTMaster",
                table: "SHGSTMaster");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHGSTMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHGSTMaster",
                table: "SHGSTMaster",
                column: "TaxID");
        }
    }
}
